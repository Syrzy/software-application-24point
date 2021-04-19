using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace software_application_24point
{
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
        private string reverse_polish_notation;
        public string ReversePolishNotation
        {
            get { return reverse_polish_notation; }
            set 
            { 
                reverse_polish_notation = value;
                if (this.PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ReversePolishNotation"));
                }
            }
        }
        public async Task StringDealAsync()
        {
            expression.Trim();//remove blank
            int length = Expression.Length;
            if (length > 0)
            {
                Stack<int> stack = new Stack<int>();
                ReversePolishNotation = null;
                int correctinput = 1;
                int flag = 0;
                int rkuo = 0, lkuo = 0;
                if(expression[0] != '+' && expression[0] != '(' && ((expression[0] < '0' || expression[0] > '9')))
                {
                    correctinput = 0;
                }
                if(expression[0] == '+' || expression[0] == '(')
                {
                    flag = 1;
                }
                for (int i = 0; i < length; i++)//flag represent how many number elements are before this element
                {
                    if ((expression[i] > '0' && expression[i] < '9') || expression[i] == '+' || expression[i] == '-' || expression[i] == '/' || expression[i] == '*' || expression[i] == '^' || expression[i] == ' ' || expression[i] == '(' || expression[i] == ')')
                    {
                        if(expression[i] > '0' && expression[i] < '9')
                        {
                            flag++;
                            if(flag > 2)
                            {
                                correctinput = 0;
                                break;
                            }
                        }
                        else
                        {
                            if(flag == 0)
                            {
                                if (expression[i - 1] == ')') 
                                {
                                    rkuo++;
                                    if(lkuo != rkuo)
                                    {
                                        correctinput = 0;
                                        break;
                                    }
                                    if(expression[i] == '(')
                                    {
                                        correctinput = 0;
                                        break;
                                    }
                                } 
                                else if(expression[i] == '(')
                                {
                                    lkuo++;
                                }
                                else
                                {
                                    correctinput = 0;
                                    break;
                                }
                            }
                            flag = 0;
                        }
                    }
                }
                if(lkuo != rkuo)
                {
                    correctinput = 0;
                }
                if (correctinput == 0)
                {
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "WRONG INPUT",
                        Content = "Check your expression.",
                        CloseButtonText = "Ok"
                    };
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    return;
                }
                else
                {
                    if (expression[0] == '+')
                    {
                        for (int i = 1; i < length; i++)
                        {
                            if(expression[i] != ' ')
                            {
                                ReversePolishNotation += Expression[i];
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < length; i++)
                        {
                            if (expression[i] != ' ')
                            {
                                ReversePolishNotation += Expression[i];
                            }
                        }
                    }
                }

                length = ReversePolishNotation.Length;
                int[] array = new int[length];
                flag = 0;
                int j = 0;
                for (int i = 0; i < length; i++,j++)//flag represent how many number elements are before this element
                {
                    if(reverse_polish_notation[i] > '9' || reverse_polish_notation[i] < '0')
                    {
                        array[++j] = (int)ReversePolishNotation[i];
                        flag = 0;
                    }
                    else
                    {
                        array[j] = (reverse_polish_notation[i] - 48)+array[j]*10;
                        flag++;
                        j--;
                    }
                }
                int[] rpn = new int[array.Length];
                int temp;
                j = 0;
                for (int i =0; i < array.Length; i++)
                {
                    if(array[i]>0 && array[i] < 14)
                    {
                        rpn[j++] = array[i];
                    }
                    else if (array[i] == '+' || array[i] == '-' || array[i] == '*' || array[i] == '/' || array[i] == '^')
                    {
                        if(stack.Count == 0)
                        {
                            stack.Push(array[i]);
                        }
                        else
                        {
                            while(stack.Count != 0)
                            {
                                temp = stack.Peek();
                                if(GetPriority(temp) >= GetPriority(array[i]))
                                {
                                    rpn[j++] =temp;
                                    stack.Pop();
                                }
                                else {break; }
                            }
                            stack.Push(array[i]);
                        }
                    }
                    else
                    {
                        if (array[i] == '(')
                        {
                            stack.Push(array[i]);
                        }
                        else
                        {
                            while (stack.Peek() != '(')
                            {
                                temp = stack.Pop();
                                rpn[j++] = temp;
                            }
                            stack.Pop();
                        }
                    }
                } 
                while (stack.Count != 0)
                {
                    temp = stack.Pop();
                    rpn[j++] = temp;
                }
            }
            //计算后缀表达式的值，默认中缀表达式所有数字都是一位的，在0-9之间  
            stack<int> mystack;
            int size = str.size();
            int num1, num2, num3;
            for (int i = 0; i < size; i++)
            {
                if (str[i] >= '0' && str[i] <= '9')
                {
                    mystack.push(str[i] - '0');
                }
                else
                {
                    num2 = mystack.top();
                    mystack.pop();
                    num1 = mystack.top();
                    mystack.pop();
                    if (str[i] == '+')
                    {
                        num3 = num1 + num2;
                    }
                    else if (str[i] == '-')
                    {
                        num3 = num1 - num2;
                    }
                    else if (str[i] == '*')
                    {
                        num3 = num1 * num2;
                    }
                    else if (str[i] == '/')
                    {
                        num3 = num1 / num2;
                    }
                    mystack.push(num3);
                }
            }
            return mystack.top();

        }
        public int GetPriority(int n)
        {
            int p= -1;
            if(n == '(') { p = 0; }
            if(n == '+' || n == '-') { p = 1; }
            if(n == '*' || n == '/') { p = 2; }
            if(n == '^') { p = 3; }
            if(n == ')') { p = 4; }
            return p;
        }
        public Input(string expression)
        {
            this.expression = expression;
        }
    }
}
