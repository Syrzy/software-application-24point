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
        User user;
        User differenceuser = new User(0,0,null);
        public GradePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter is User)
            {
                user = e.Parameter as User;
                PresentWin.DataContext = differenceuser;
                TotalWin.DataContext = user;
                PresentLose.DataContext = differenceuser;
                TotalLose.DataContext = user;
                UserName.DataContext = user;
                SetLocalData();
            }
        }
        private void SetLocalData()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            int UserNumber = Convert.ToInt32(localSettings.Values["IndexValue"].ToString());
            for (int i = 0; i < UserNumber; i++)
            {
                try
                {
                    Windows.Storage.ApplicationDataCompositeValue UserValue = localSettings.Values[i.ToString()] as Windows.Storage.ApplicationDataCompositeValue;
                    if (UserValue["name"].ToString() == user.Name)
                    {
                        differenceuser.Wintimes = user.Wintimes - Convert.ToInt32(UserValue["wintimes"].ToString());
                        differenceuser.Losetimes = 5-differenceuser.Wintimes;
                        user.Losetimes += differenceuser.Losetimes;
                        UsersCollection.Users[i] = user;
                        UserValue["wintimes"] = user.Wintimes;
                        UserValue["losetimes"] = user.Losetimes;
                        localSettings.Values[i.ToString()] = UserValue;
                        break;
                    }
                }
                catch { }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage),user);
        }
    }
}
