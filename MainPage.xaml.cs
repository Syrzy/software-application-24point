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

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace software_application_24point
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int PlayTime = 5;
        Input input = new Input("Please type in arithmetic expression here.");
        Solve solve = new Solve();
        User user = new User(0, 0, "NULL");
        public MainPage()
        {
            this.InitializeComponent();
            InputTextBox.DataContext = input;
            RPN.DataContext = solve;
            solve.ProduceRandomNumber();
            B1.DataContext = solve;
            B2.DataContext = solve;
            B3.DataContext = solve;
            B4.DataContext = solve;
        }

        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            _ = input.StringDealAsync();
            if (input.GetandSetArray != null)
            {
                _=solve.Judge(input.CaculationResult,input.GetandSetArray);
            }
            if(solve.Correct == true)
            {
                user.Wintimes++;
            }
            if(--PlayTime == 0)
            {
                Frame.Navigate(typeof(GradePage),user.Wintimes);
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
                    if (--PlayTime == 0)
                    {
                        Frame.Navigate(typeof(GradePage), user.Wintimes);
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
                    if (--PlayTime == 0)
                    {
                        Frame.Navigate(typeof(GradePage), user.Wintimes);
                    }
                }
            }
        }
    }
}
