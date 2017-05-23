using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.IO.IsolatedStorage;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Windows.Phone.Input;
using Microsoft.Devices;
using Microsoft.Phone.Shell;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace TasbihZikr
{
    public partial class StartZikr : PhoneApplicationPage
    {
        VibrateController testVibrateController = VibrateController.Default;
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        public StartZikr()
        {
            InitializeComponent();
            if (settings.Contains("counting") || GlobalValues.counting > 0)
                if (GlobalValues.counting > 0) countBlock.Text = GlobalValues.counting.ToString();
                else try { GlobalValues.counting = Convert.ToInt32((string)settings["counting"]); }
                catch { };
                countBlock.Text = GlobalValues.counting.ToString();
        }
       
        private async void setAlert_Click(object sender, RoutedEventArgs e)
        {
            InputPromptSettings ips = new InputPromptSettings();
            Style usernameStyle = new System.Windows.Style(typeof(TextBox));
            usernameStyle.Setters.Add(new Setter(TextBox.InputScopeProperty, "Number"));
            ips.Field1Mode = InputMode.Text;
            string messageTitle = "Count Limit!";
            string message = "Enter the No.of repetitions at which you want to get alerted";
            if (GlobalValues.alertCount > 0 || settings.Contains("alertCount"))
            {
                //MessageBox.Show("There is a AlertCountValue"); //Checking IT!
                string valu = string.Empty;
                if (GlobalValues.alertCount>0) 
                {
                    valu = GlobalValues.alertCount.ToString();
                }
                else
                {
                    try { valu = ((string)settings["alertCount"]); }
                    catch { }
                }
                usernameStyle.Setters.Add(new Setter(RadTextBox.TextProperty, valu));
            }
            else {  }
            ips.Field1Style = usernameStyle;
            InputPromptClosedEventArgs args = await RadInputPrompt.ShowAsync(ips, messageTitle, MessageBoxButtons.OKCancel, message);
            if (args.Result == DialogResult.OK)
            {
                try { GlobalValues.alertCount = Convert.ToInt32(args.Text);}
                catch { }
            }
        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            GlobalValues.counting = 0;
            if (!settings.Contains("counting")) settings.Add("counting","0");
            else settings["counting"] = GlobalValues.counting.ToString();
            settings.Save();
            countBlock.Text = GlobalValues.counting.ToString();
        }

        private void LayoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GlobalValues.counting = GlobalValues.counting + 1;
            countBlock.Text = GlobalValues.counting.ToString();
            if (GlobalValues.alertCount > 0 && GlobalValues.counting == GlobalValues.alertCount)
            {
                testVibrateController.Start(TimeSpan.FromSeconds(2));
                MessageBox.Show("Alerted!");
            }
        }

        private void PhoneApplicationPage_BeginLayoutChanged(object sender, OrientationChangedEventArgs e)
        {

        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}