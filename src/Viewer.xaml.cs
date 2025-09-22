using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Devices.AllJoyn;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Selectivitapp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Viewer : Page
    {
        bool answers = false;
        int year = 2024;
        string month = "";
        private Subject? subject;
        public Viewer()
        {
            this.InitializeComponent();
            updateGrid();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string code= e.Parameter as string ?? "";
            subject = Subjects.General.FirstOrDefault(s => s.Code == code) 
                ?? Subjects.Languages.FirstOrDefault(s => s.Code == code)
                ?? Subjects.Science.FirstOrDefault(s => s.Code == code)
                ?? Subjects.Social.FirstOrDefault(s => s.Code == code)
                ?? Subjects.Artistic.FirstOrDefault(s => s.Code == code);

            if (subject is null) return;
            PDFTitle.Text = subject.Name ?? "";
            
            YearSelector.SelectionChanged += YearSelector_ValueChanged;
            for(int y = subject.MaxYear; y >= subject.MinYear; y--)
                YearSelector.Items.Add(y);
            YearSelector.SelectedIndex = 0;

            loadPDF();
            base.OnNavigatedTo(e);
        }

        bool wbloaded = false;
        
        
        async void loadPDF()
        {
            if (ViewEnunciats is null || ViewSolucions is null) return;

            if (!wbloaded)
            {
                await ViewEnunciats.EnsureCoreWebView2Async();
                await ViewSolucions.EnsureCoreWebView2Async();
                ViewEnunciats.NavigationCompleted += WebView_Loaded;
                ViewSolucions.NavigationCompleted += WebView_Loaded;
                wbloaded = true;
            }
            pgsBar.Visibility = Visibility.Visible;
            ViewEnunciats.Source = new Uri(getURL(false));
            ViewSolucions.Source = new Uri(getURL(true));
        }

        private bool showingErrorDialog;
        private async void WebView_Loaded(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.HttpStatusCode is 0 && e.WebErrorStatus is CoreWebView2WebErrorStatus.ConnectionAborted) 
                 return; // Not loaded yet

            await Task.Delay(250);
            pgsBar.Visibility = Visibility.Collapsed;
            if(e.HttpStatusCode is not 200)
            {
                if (showingErrorDialog) return;
                showingErrorDialog = true;
                var dialog = new ContentDialog
                {
                    Title = "No s'ha pogut carregar el document.",
                    Content = $"No s'ha pogut carregar l'examen sol·licitat. ({e.WebErrorStatus}, Codi {e.HttpStatusCode}). Comproveu la connexió a internet o torneu a intentar-ho més tard.",
                    CloseButtonText = "Tanca",
                    XamlRoot = this.XamlRoot
                };
                await dialog.ShowAsync();
                showingErrorDialog = false;
            }
        }

        string getURL(bool answers)
        {
            if (subject is null) return "about:blank";
            if (year is 0) year = subject.MaxYear;
            else if (year < subject.MinYear) year = subject.MinYear;
            else if (year > subject.MaxYear) year = subject.MaxYear ;
            if (month == "") month = "Juny";
            
            string monthLetter = (month == "Setembre")? "s": "j";
            string answersLetter = answers? "p": "l";

            //string BaseString = $"https://marticliment.com/selectivitapp/?pdf=pau_{subject.Code}{year.ToString()[^2..]}{monthLetter}{answersLetter}.pdf";
            string BaseString = $"https://marticliment.com/selectivitapp/pau_{subject.Code}{year.ToString()[^2..]}{monthLetter}{answersLetter}.pdf";
            Debug.WriteLine(BaseString);
            return BaseString;
        }

        private void Answers_SelectionChanged(object sender, SelectionChangedEventArgs e)
           =>  updateGrid();
        
        private void updateGrid()
        {
            if (Answers is null || ColumnEnunciats is null || ColumnSolucions is null || LeftBorder is null || RightBorder is null) return;

            switch (Answers.SelectedIndex)
            {
                case 1: // Solucions
                    ColumnEnunciats.Width = new GridLength(0);
                    ColumnSolucions.Width = new GridLength(1, GridUnitType.Star);
                    LeftBorder.Margin = new Thickness(10, 0, 10, 10);
                    RightBorder.Margin = new Thickness(10, 0, 10, 10);
                    break;
                case 0: // Enunciats
                    ColumnSolucions.Width = new GridLength(0);
                    ColumnEnunciats.Width = new GridLength(1, GridUnitType.Star);
                    LeftBorder.Margin = new Thickness(10, 0, 10, 10);
                    RightBorder.Margin = new Thickness(10, 0, 10, 10);
                    break;
                case 2: // Pantalla dividida:
                    ColumnSolucions.Width = new GridLength(1, GridUnitType.Star);
                    ColumnEnunciats.Width = new GridLength(1, GridUnitType.Star);
                    LeftBorder.Margin = new Thickness(10, 0, 5, 10);
                    RightBorder.Margin = new Thickness(5, 0, 10, 10);
                    break;
            }
        }

        private void MonthSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            month = MonthSelector.SelectedValue as string ?? "";
            loadPDF();
        }

        private void YearSelector_ValueChanged(object sender, SelectionChangedEventArgs args)
        {
            year = (int)YearSelector.SelectedValue;
            loadPDF();
        }

        private void YearDown_Click(object sender, RoutedEventArgs e)
        {
            if (YearSelector.SelectedIndex < YearSelector.Items.Count-1) 
                YearSelector.SelectedIndex++;
        }

        private void YearUp_Click(object sender, RoutedEventArgs e)
        {
            if (YearSelector.SelectedIndex > 0)
                YearSelector.SelectedIndex--;
        }
    }
}
