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
            YearSelector.Maximum = subject.MaxYear;
            YearSelector.Value = subject.MaxYear;
            YearSelector.Minimum = subject.MinYear ;
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
                wbloaded = true;
            }
            pgsBar.Visibility = Visibility.Visible;
            ViewEnunciats.Source = new Uri(getURL(false));
            ViewSolucions.Source = new Uri(getURL(true));
            ViewEnunciats.NavigationCompleted += WebView_Loaded;
            ViewSolucions.NavigationCompleted += WebView_Loaded;
        }

        private void WebView_Loaded(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            pgsBar.Visibility = Visibility.Collapsed;
        }

        string getURL(bool answers)
        {
            if (subject is null) return "about:blank";
            if (year == 0) year = 2024;
            if (month == "") month = "Juny";
            
            string monthLetter = (month == "Setembre")? "s": "j";
            string answersLetter = answers? "p": "l";
            
            string BaseString = $"https://cdn.jsdelivr.net/gh/marticliment/Selectivitapp@main/docs/pau_{subject.Code}{year%100}{monthLetter}{answersLetter}.pdf";
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

        private void YearSelector_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            year = (int)YearSelector.Value;
            loadPDF();
        }
    }
}
