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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Selectivitapp.Assets
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Viewer : Page
    {
        bool answers = false;
        string examen = "";
        public Viewer()
        {
            this.InitializeComponent();
            loadPDF();
            Month.Items.Clear();
            int anyActual = 2022;
            for(int i = anyActual; i>=2000; i--)
            {
                Month.Items.Add("Juny de "+i.ToString());
                Month.Items.Add("Setembre de "+i.ToString());
            }
            Month.SelectedValue = "Juny de " + anyActual.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).navigationFrame.GoBack();

        }

        async void loadPDF()
        {
            await ViewEnunciats.EnsureCoreWebView2Async();
            await ViewSolucions.EnsureCoreWebView2Async();
            pgsBar.Visibility = Visibility.Visible;
            ViewEnunciats.Source = new Uri(getURL(examen, false));
            ViewSolucions.Source = new Uri(getURL(examen, true));
            PDFTitle.Text = ((App)Application.Current).nom;
            ViewEnunciats.NavigationCompleted += WebView_Loaded;
            ViewSolucions.NavigationCompleted += WebView_Loaded;
        }

        private void WebView_Loaded(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            pgsBar.Visibility = Visibility.Collapsed;
        }

        string getURL(string year, bool answers)
        {
            if (year.Length <= 2)
            {
                year = "Juny de 2022";
            }
            string monthLetter = "j";
            if(year.Contains("Setembre")) {
                monthLetter = "s";
            }
            string answersLetter = "l";
            if(answers)
            {
                answersLetter = "p";
            }
            string BaseString = "https://www.selecat.cat/pau/pau_" + ((App)Application.Current).codi + year[year.Length - 2] + year[year.Length - 1] + monthLetter + answersLetter + ".pdf";
            Debug.WriteLine(BaseString);
            return BaseString;
        }

        private void Answers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is RadioButtons rb)
            {
                string answer = rb.SelectedItem as string;
                switch (answer)
                {
                    case "Solucions":
                        answers = true;
                        break;
                    case "Enunciats":
                        answers = false;
                        break;
                }
                updateGrid();
            }
        }
        private void updateGrid()
        {
            if (Answers is RadioButtons rb)
            {
                string answer = rb.SelectedItem as string;
                switch (answer)
                {
                    case "Solucions":
                        ColumnEnunciats.Width = new GridLength(0);
                        ColumnSolucions.Width = new GridLength(1, GridUnitType.Star);
                        LeftBorder.Margin = new Thickness(10, 0, 10, 10);
                        RightBorder.Margin = new Thickness(10, 0, 10, 10);
                        break;
                    case "Enunciats":
                        ColumnSolucions.Width = new GridLength(0);
                        ColumnEnunciats.Width = new GridLength(1, GridUnitType.Star);
                        LeftBorder.Margin = new Thickness(10, 0, 10, 10);
                        RightBorder.Margin = new Thickness(10, 0, 10, 10);
                        break;
                    case "Pantalla dividida":
                        ColumnSolucions.Width = new GridLength(1, GridUnitType.Star);
                        ColumnEnunciats.Width = new GridLength(1, GridUnitType.Star);
                        LeftBorder.Margin = new Thickness(10, 0, 5, 10);
                        RightBorder.Margin = new Thickness(5, 0, 10, 10);
                        break;
                }
            }
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (sender is ComboBox cb)
            {
                examen = cb.SelectedValue as string;
                Debug.WriteLine(examen);
                loadPDF();
            }
        }
    }
}
