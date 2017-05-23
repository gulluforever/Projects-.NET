using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TasbihZikr.Resources;

namespace TasbihZikr
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Mode = ApplicationBarMode.Default;
            ApplicationBar.Opacity = 1.0;
            ApplicationBar.BackgroundColor = Colors.Black;
            ApplicationBar.IsVisible = true;
            ApplicationBar.IsMenuEnabled = true;
            ApplicationBarMenuItem menuItem = new ApplicationBarMenuItem();
            menuItem.Text = "Asma ul Husna";
            menuItem.Click += menuItem_Click;
            ApplicationBarMenuItem menuItem1 = new ApplicationBarMenuItem();
            menuItem1.Text = "Masnoon Duain";
            ApplicationBar.MenuItems.Add(menuItem);
            ApplicationBar.MenuItems.Add(menuItem1);
            menuItem1.Click += new EventHandler(menuItem1_Click);
            ApplicationBarIconButton rate = new ApplicationBarIconButton();
            rate.IconUri = new Uri("/Assets/AppButtons/heart.png", UriKind.Relative);
            rate.Text = "Rate App";
            rate.Click += rate_Click;
            ApplicationBar.Buttons.Add(rate);
        }

        void menuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AsmaulHusna.xaml", UriKind.Relative));
        }
        void rate_Click(object sender, EventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }
        void menuItem1_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MasnoonDuain.xaml", UriKind.Relative));
        }
        private void starZikr_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/StartZikr.xaml", UriKind.Relative)); //Getting Navigated to the StartZikr Page!
        }


        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}