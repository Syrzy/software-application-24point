using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace software_application_24point
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class GradePage : Page
    {
        User user = new User(0, 0, "NULL");
        public GradePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter is int)
            {
                user.Wintimes = (int)e.Parameter;
                Present.DataContext = user;
                Total.DataContext = user;
                GetandSetLocalData();
            }
        }
        private void GetandSetLocalData()
        {
            int UserNumber = 0;
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values["IndexValue"] == null)
            {
                user.Totalwintimes = user.Wintimes;
            }
            else
            {
                UserNumber = Convert.ToInt32(localSettings.Values["IndexValue"].ToString());
                UsersCollection.Users.Clear();
                for (int i = 1; i < UserNumber; i++)
                {
                    if (localSettings.Values[i.ToString()] != null)
                    {
                        Windows.Storage.ApplicationDataCompositeValue UserValue = localSettings.Values[i.ToString()] as Windows.Storage.ApplicationDataCompositeValue;
                        UsersCollection.Users.Add(new User(Convert.ToInt32(UserValue["totalwintimes"]), Convert.ToInt32(UserValue["wintimes"]), UserValue["name"].ToString()));
                    }
                }
                user.Totalwintimes = UsersCollection.Users[0].Totalwintimes;
                UsersCollection.Users[0].Totalwintimes += user.Wintimes;
                UsersCollection.Users[0].Wintimes = user.Wintimes;
            }

            User testUser;
            for (int i = 0; i < UserNumber; i++)
            {
                try
                {
                    Windows.Storage.ApplicationDataCompositeValue UserValue = localSettings.Values[i.ToString()] as Windows.Storage.ApplicationDataCompositeValue;
                    testUser = new User(Convert.ToInt32(UserValue["totalwintimes"]), Convert.ToInt32(UserValue["wintimes"]), UserValue["name"].ToString());
                    if (testUser.Totalwintimes == user.Totalwintimes && testUser.Wintimes == user.Wintimes)
                    {
                        localSettings.Values[i.ToString()] = UsersCollection.Users[0];
                    }
                }
                catch { }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
