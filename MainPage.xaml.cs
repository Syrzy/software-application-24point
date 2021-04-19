using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public MainPage()
        {
            this.InitializeComponent();
            Input input = new Input("Please type in arithmetic expression here.");
            InputTextBox.DataContext = input;
            RPN.DataContext = input;
        }
    }
    class Input : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string expression;
        public string Expression
        {
            get { return expression; }
            set
            {
                expression = value;
                if (this.PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Expression"));
                }
            }
        }
        public string ReversePolishNotation;
        public string StringDeal()
        {
            get { return ReversePolishNotation; }
            Stack<string> stack = new Stack<string>();
            int length = expression.Length;
            for (int i = 0; i < length; i++)
            {
                if()
            }
        }
        public Input(string expression)
        {
            this.expression = expression;
        }
    }
}
