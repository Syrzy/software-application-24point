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

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace software_application_24point
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Input input = new Input("Please type in arithmetic expression here.");
        Solve solve = new Solve();
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
                solve.Judge(input.CaculationResult,input.GetandSetArray);
            }
            
        }

        private void Renew_Button_Click(object sender, RoutedEventArgs e)
        {
            solve.ProduceRandomNumber();
        }

        private void Help_Button_Click(object sender, RoutedEventArgs e)
        {
            solve.FindAllSolution(0);
        }
    }
}
