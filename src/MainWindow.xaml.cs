using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Selectivitapp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public Collection<NavigationViewItemBase> NavBarItems = new();
        public MainWindow()
        {
            LoadMenuBarItems();
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
            NavigationFrame.Navigate(typeof(HomePage));
        }

        private void LoadMenuBarItems()
        {
            NavBarItems.Add(new NavigationViewItem()
            {
                Content = "Inici",
                Tag = "home",
                Icon = new SymbolIcon(Symbol.Home)
            });

            foreach (var list in new[] { Subjects.General, Subjects.Languages, Subjects.Science, Subjects.Social, Subjects.Artistic })
            {
                NavBarItems.Add(new NavigationViewItemSeparator());
                foreach (var item in list)
                {
                    NavBarItems.Add(new NavigationViewItem()
                    {
                        Content = item.Name,
                        Tag = item.Code,
                        Icon = new BitmapIcon() { UriSource = new Uri(item.IconPath) }
                    });
                }
            }
        }
        
        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if(args.InvokedItemContainer is NavigationViewItem i)
            {
                if ((string)i.Tag == "home")
                {
                    NavigationFrame.Navigate(typeof(HomePage));
                }
                else
                {
                    NavigateToTag((string)i.Tag);
                }
            }
        }

        private void AppTitleBar_BackRequested(TitleBar sender, object args)
        {
            NavigationFrame.GoBack();
        }

        private void AppTitleBar_PaneToggleRequested(TitleBar sender, object args)
        {
            NavigationView.IsPaneOpen = !NavigationView.IsPaneOpen;
        }

        public void NavigateToTag(string tag)
        {
            NavigationFrame.Navigate(typeof(Viewer), tag);
            NavigationView.IsPaneOpen = false;
        }
    }
}
