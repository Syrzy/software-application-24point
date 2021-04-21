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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace software_application_24point
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BeginPage : Page
    {
        User user = new User(0, 0, null);//
        string username;
        public BeginPage()
        {
            this.InitializeComponent();
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/BackGround.jpg", UriKind.Absolute));
            BeginPageGrid.Background = imageBrush;
        }
        private void GetLocalData()
        {
            int UserNumber = 0;
            username = TextBoxUserName.Text;
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values["IndexValue"] == null)
            {
                UserNumber++;
                user.Name = TextBoxUserName.Text;
                UsersCollection.Users.Clear();
                UsersCollection.Users.Add(user);
                localSettings.Values["IndexValue"] = 1;
                Windows.Storage.ApplicationDataCompositeValue Composite = new Windows.Storage.ApplicationDataCompositeValue();
                Composite["name"] = user.Name;
                Composite["losetimes"] = user.Losetimes;
                Composite["wintimes"] = user.Wintimes;
                localSettings.Values["0"] = Composite;
            }
            else
            {
                UserNumber = Convert.ToInt32(localSettings.Values["IndexValue"].ToString());
                UsersCollection.Users.Clear();
                for (int i = 0; i < UserNumber; i++)
                {
                    if (localSettings.Values[i.ToString()] != null)
                    {
                        Windows.Storage.ApplicationDataCompositeValue UserValue = localSettings.Values[i.ToString()] as Windows.Storage.ApplicationDataCompositeValue;
                        if (UserValue["name"].ToString() == TextBoxUserName.Text)
                        {
                            user.Name = UserValue["name"].ToString();
                            user.Losetimes = Convert.ToInt32(UserValue["losetimes"].ToString());
                            user.Wintimes = Convert.ToInt32(UserValue["wintimes"].ToString());
                            UsersCollection.Users.Add(user);
                        }
                        else
                        {
                            user.Name = UserValue["name"].ToString();
                            user.Losetimes = Convert.ToInt32(UserValue["losetimes"].ToString());
                            user.Wintimes = Convert.ToInt32(UserValue["wintimes"].ToString());
                            UsersCollection.Users.Add(user);
                            user.Name = null;
                        }
                    }
                }
                if (user.Name == null)
                {
                    user.Name = TextBoxUserName.Text;
                    user.Losetimes = 0;
                    user.Wintimes = 0;
                    UsersCollection.Users.Add(user);
                    Windows.Storage.ApplicationDataCompositeValue Composite = new Windows.Storage.ApplicationDataCompositeValue();
                    Composite["name"] = user.Name;
                    Composite["losetimes"] = user.Losetimes;
                    Composite["wintimes"] = user.Wintimes;
                    localSettings.Values[UserNumber.ToString()] = Composite;
                    localSettings.Values["IndexValue"] = (UserNumber + 1).ToString();
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetLocalData();
            Frame.Navigate(typeof(MainPage), user);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values.Remove("IndexValue");
            localSettings.Values.Remove("0");
        }
    }
}
