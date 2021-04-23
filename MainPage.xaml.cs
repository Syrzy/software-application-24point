using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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
using Windows.UI.Popups;
using Windows.System.Threading;
using Windows.UI.Xaml.Media.Imaging;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace software_application_24point
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ThreadPoolTimer _timer;//time counter,let the last ContentDialogResult can be chose and deal
        int PlayTime = 5;//how many rounds in one game
        Input input = new Input("");//creat a Input entity
        Solve solve = new Solve();
        User user;//user will gain data from the past page
        public MainPage()
        {
            this.InitializeComponent();

            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/BackGround.jpg", UriKind.Absolute));
            MainPageGrid.Background = imageBrush;

            InputTextBox.DataContext = input;//binding. Through the binding input entity get the user input
            RPN.DataContext = solve;//binding

            solve.ProduceRandomNumber();//proudce 4 random numbers from palyer. ProduceRandomNumber() will assure these 4 numbers can make up 24
            ChangeImage();//in line with random numbers change the photos

            B1.DataContext = solve;//binding
            B2.DataContext = solve;
            B3.DataContext = solve;
            B4.DataContext = solve;

        }

        public void ChangeImage()//changes the address of Image Control in line with 4 numbers.
        {
            Image1.Source = new BitmapImage(new Uri(GetAddress(Convert.ToInt32(solve.A1))));
            Image2.Source = new BitmapImage(new Uri(GetAddress(solve.A2)));
            Image3.Source = new BitmapImage(new Uri(GetAddress(solve.A3)));
            Image4.Source = new BitmapImage(new Uri(GetAddress(solve.A4)));
        }
        public string GetAddress(int n)//on the basis of number(1-13) transform number into address
        {
            string address = "ms-appx:///Assets/";
            switch (n)
            {
                case 1: address += "黑桃A.jpg"; break;
                case 2: address += "黑桃2.jpg"; break;
                case 3: address += "黑桃3.png"; break;
                case 4: address += "黑桃4.png"; break;
                case 5: address += "黑桃5.jpg"; break;
                case 6: address += "黑桃6.jpg"; break;
                case 7: address += "黑桃7.jpg"; break;
                case 8: address += "黑桃8.jpg"; break;
                case 9: address += "黑桃9.jpg"; break;
                case 10: address += "黑桃10.jpg"; break;
                case 11: address += "黑桃J.jpg"; break;
                case 12: address += "黑桃Q.jpg"; break;
                case 13: address += "黑桃K.jpg"; break;
            }
            return address;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter is User)
            {
                user = e.Parameter as User;//gain the data deliveried from the past page
            }
        }

        private void Submit_Button_Click(object sender, RoutedEventArgs e)//user submit his input
        {
            _ = input.StringDealAsync();//transform the input into reverse polish notation and judge the legality of input 
            if (input.GetandSetArray != null)
            {
                _=solve.Judge(input.CaculationResult,input.GetandSetArray);//judge the caculation result of RPN and modify the value of solve.Correct
            }
            if(solve.Correct == true)
            {
                user.Wintimes++;
                solve.Correct = false;
                ChangeImage();//in the function solve.Judge if result is correct, it will invoke ProduceRandomNumber
            }
            if(--PlayTime <= 0)//every submit will cost one play chance
            {
                bool jump = true;
                _timer = ThreadPoolTimer.CreateTimer(
                    (timer) =>
                    {
                        jump = false;
                    },
                    TimeSpan.FromSeconds(2));
                while (jump) ;
                Frame.Navigate(typeof(GradePage), user);//the function that need to delay
            }
        }
        private async void Renew_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msg = new MessageDialog("Renovate will cost you one chance, are you sure?");

            UICommand cmdYes = new UICommand();
            cmdYes.Id = 1;
            cmdYes.Label = "Yes";

            UICommand cmdNo = new UICommand();
            cmdNo.Id = 2;
            cmdNo.Label = "No";

            msg.Commands.Add(cmdNo);
            msg.Commands.Add(cmdYes);

            var selectedCommand = await msg.ShowAsync();
            if(selectedCommand != null)
            {
                if ((int)selectedCommand.Id == 1)
                {
                    solve.ProduceRandomNumber();
                    ChangeImage();
                    if (--PlayTime <= 0)
                    {
                        MessageDialog msg1 = new MessageDialog("You have used up your chances!\nGame is Over!\n");//tell the user he has run out of chances;
                        UICommand cmdYes1 = new UICommand();
                        cmdYes1.Id = 1;
                        cmdYes1.Label = "Let's see the grades.";
                        msg1.Commands.Add(cmdYes1);
                        var selectedCommand1 = await msg1.ShowAsync();
                        Submit.IsEnabled = false; 
                        if ((int)selectedCommand1.Id == 1)
                        {
                            Frame.Navigate(typeof(GradePage), user);
                        }
                    }
                }
            }
        }
        private async void Help_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msg = new MessageDialog("Seeking help will cost you one chance, are you sure?");

            UICommand cmdYes = new UICommand();
            cmdYes.Id = 1;
            cmdYes.Label = "Yes";

            UICommand cmdNo = new UICommand();
            cmdNo.Id = 2;
            cmdNo.Label = "No";

            msg.Commands.Add(cmdNo);
            msg.Commands.Add(cmdYes);

            var selectedCommand = await msg.ShowAsync();
            if (selectedCommand != null)
            {
                if ((int)selectedCommand.Id == 1)
                {
                    solve.FindAllSolution();
                    //solve.ProduceRandomNumber();
                    if (--PlayTime <= 0)
                    {
                        solve.AllSolution = "You have used up your chances!\n" + solve.AllSolution;
                        MessageDialog msg1 = new MessageDialog("Game is Over!");
                        UICommand cmdYes1 = new UICommand();
                        cmdYes1.Id = 1;
                        cmdYes1.Label = "Let's see the grades.";
                        msg1.Commands.Add(cmdYes1);
                        var selectedCommand1 = await msg1.ShowAsync();
                        Submit.IsEnabled = false;
                        if ((int)selectedCommand1.Id == 1)
                        {
                            Frame.Navigate(typeof(GradePage), user);
                        }
                    }
                }
            }
        }
    }
}
