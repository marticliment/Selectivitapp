using Selectivitapp.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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

        private void click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).codi = GetValue(CodeProperty).ToString();
            ((App)Application.Current).nom = TextBlock.Text;
            ((App)Application.Current).navigationFrame.Navigate(typeof(Viewer));
        }

    }
}
