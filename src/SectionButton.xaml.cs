using Selectivitapp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using src;
using Windows.Web.Http.Headers;
using Selectivitapp.Assets;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Selectivitapp
{
    public sealed partial class SectionButton : UserControl
    {
        private DependencyProperty TextProperty = DependencyProperty.RegisterAttached(
         "Text",
         typeof(string),
         typeof(SectionButton),
         new PropertyMetadata(""));

        private DependencyProperty CodeProperty = DependencyProperty.RegisterAttached(
         "Code",
         typeof(string),
         typeof(SectionButton),
         new PropertyMetadata(""));

        private DependencyProperty IconProperty = DependencyProperty.RegisterAttached(
         "Icon",
         typeof(string),
         typeof(SectionButton),
         new PropertyMetadata(""));


        public String Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public String Code
        {
            get { return (string)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }

        public String Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public SectionButton()
        {
            this.InitializeComponent();
            Loaded += SectionButton_Loaded;            
        }


        private void SectionButton_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock.Text = GetValue(TextProperty).ToString();
            ContentIcon.UriSource = new Uri(GetValue(IconProperty).ToString());
        }

        public static string Codi { get; private set; } = "";
        public static string Nom { get; private set; } = "";

        private void click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current)?._window?.NavigateToTag(GetValue(CodeProperty)?.ToString() ?? "");
        }

    }
}
