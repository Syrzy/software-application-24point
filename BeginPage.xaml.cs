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
            this.InitializeComponent();//page is initialized here
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/BackGround.jpg", UriKind.Absolute));
            BeginPageGrid.Background = imageBrush;//creat a image background
        }
        private void GetLocalData()
        {
            int UserNumber = 0;//represents how many user datas are there in the localsetting
            username = TextBoxUserName.Text;//此处存在bug。若用户输入为空则引发错误，需修正。
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;//call a basic database
            if (localSettings.Values["IndexValue"] == null)//means there is No user before
            {
                //the sentences below are aimed to creat a new user data and storage in the localsetting 
                UserNumber++;
                user.Name = TextBoxUserName.Text;
                UsersCollection.Users.Clear();//UserCollection暂时无用，未来添加用户数据修改功能时有用
                UsersCollection.Users.Add(user);
                localSettings.Values["IndexValue"] = 1;//the key "IndexValue" will storage the number of user data
                Windows.Storage.ApplicationDataCompositeValue Composite = new Windows.Storage.ApplicationDataCompositeValue();//use Composite to modify the value of localsetting
                Composite["name"] = user.Name;
                Composite["losetimes"] = user.Losetimes;
                Composite["wintimes"] = user.Wintimes;
                localSettings.Values["0"] = Composite;//the key"0" means this user data is the first.in the future if there are more user datas,the key will increase from "0",like "1","2"……
            }
            else//if exists user data
            {
                UserNumber = Convert.ToInt32(localSettings.Values["IndexValue"].ToString());
                UsersCollection.Users.Clear();// temporarily no use
                for (int i = 0; i < UserNumber; i++)
                {
                    if (localSettings.Values[i.ToString()] != null)
                    {
                        Windows.Storage.ApplicationDataCompositeValue UserValue = localSettings.Values[i.ToString()] as Windows.Storage.ApplicationDataCompositeValue;//get the value of localsetting in order of "0""1""2"......
                        if (UserValue["name"].ToString() == TextBoxUserName.Text)//means find this user has been recorded before
                        {
                            user.Name = UserValue["name"].ToString();//copy the local data into element user
                            user.Losetimes = Convert.ToInt32(UserValue["losetimes"].ToString());
                            user.Wintimes = Convert.ToInt32(UserValue["wintimes"].ToString());
                            UsersCollection.Users.Add(user);
                        }
                        else//else 此处其实也没用，仅用于为UserCollection添加数据
                        {
                            user.Name = UserValue["name"].ToString();
                            user.Losetimes = Convert.ToInt32(UserValue["losetimes"].ToString());
                            user.Wintimes = Convert.ToInt32(UserValue["wintimes"].ToString());
                            UsersCollection.Users.Add(user);
                            user.Name = null;
                        }
                    }
                }
                if (user.Name == null)//after the above progresses，if user name still empty, means this time the user is a new one
                {
                    user.Name = TextBoxUserName.Text;
                    user.Losetimes = 0;
                    user.Wintimes = 0;
                    UsersCollection.Users.Add(user);
                    Windows.Storage.ApplicationDataCompositeValue Composite = new Windows.Storage.ApplicationDataCompositeValue();//storage the new user data into localsetting
                    Composite["name"] = user.Name;
                    Composite["losetimes"] = user.Losetimes;
                    Composite["wintimes"] = user.Wintimes;
                    localSettings.Values[UserNumber.ToString()] = Composite;
                    localSettings.Values["IndexValue"] = (UserNumber + 1).ToString();
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)//this button will lead to the mainpage
        {
            GetLocalData();//before user goes to the next page, program deals with the user data
            Frame.Navigate(typeof(MainPage), user);//delivery the user entity to the MainPage, so as to record the information of game
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//没用的按钮，调试程序时使用，未来可删除
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values.Remove("IndexValue");//delete the key-value pair
            localSettings.Values.Remove("0");
        }
    }
}
