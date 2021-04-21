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
    class Input : INotifyPropertyChanged//for Controls to implement binding
    {
        public event PropertyChangedEventHandler PropertyChanged;
        /*事实上，未来修正中此处接口可以取消。在最终程序中不需绑定。但由于调试过程所需因此遗留。*/
        private int[] array;//array storage the ASCII code of user input
        public int[] GetandSetArray
        {
            get { return array; }
            set { array = value; }
        }
        private double caculationresult;//this member represents the caculation result of user input
        public double CaculationResult
        {
            get { return caculationresult; }
            set { caculationresult = value; }
        }
        private int[] rpn;//rpn storages the reversed polish notation of user input，operations are saved in form of ASCII
        public int[] Rpn 
        { 
            get { return rpn; }
            set { rpn = value; } 
        }
        private string expression;//expression binding with the text property of a textcontrol,it represents the string input
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
        private string reverse_polish_notation;//storage the reversed Polish notation in string form
        public string ReversePolishNotation
        {
            get { return reverse_polish_notation; }
            set 
            { 
                reverse_polish_notation = value;
                if (this.PropertyChanged != null)//由于不再绑定，这里实现的接口同样没有意义了，仅用于软件测试
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ReversePolishNotation"));
                }
            }
        } 
        public async Task StringDealAsync()//including  ContentDialog, which needs async symbol
        {
            Expression.Trim();//remove blank
            int length = Expression.Length;//we need to traverse the entire string and judge every character.
            if (length > 0)//else the input is null
            {
                Stack<int> stack = new Stack<int>();//this stack is used to convert rpn to nifix expression, and the inversion.
                ReversePolishNotation = null;//represents the string form of rpn
                int correctinput = 1;//1 means input is legal.0 means the input is illegal.
                int flag = 0;//flag represent how many number elements are before this element
                int rkuo = 0, lkuo = 0;//rkuo means the number of right bracket; lkuo means the number of left bracket.
                int number = 0;//number record the number of numbers in input.
                if(expression[0] != '+' && expression[0] != '(' && ((expression[0] < '0' || expression[0] > '9')))//judge the first character
                {
                    correctinput = 0;
                }
                if(expression[0] == '+' || expression[0] == '(')//if the first character is not a number, 
                {
                    flag = 1;//we assume there is a hidding number before.
                }
                for (int i = 0; i < length; i++)//flag represent how many number elements are before this element
                {
                    /*we only admit the characters below：10 kinds of numbers and five kinds of operations*/
                    if ((expression[i] >= '0' && expression[i] <= '9') || expression[i] == '+' || expression[i] == '-' || expression[i] == '/' || expression[i] == '*' || expression[i] == '^' || expression[i] == ' ' || expression[i] == '(' || expression[i] == ')')
                    {
                        if(expression[i] >= '0' && expression[i] <= '9')
                        {
                            number++;
                            flag++;//flag can help us define the digit of input number
                            if(flag > 2)//flag > 2, means there is a 3-digit-number
                            {
                                correctinput = 0;
                                break;
                            }
                        }
                        else
                        {
                            if(flag == 0)//means there is an opreartion before
                            {
                                if (expression[i - 1] == ')') 
                                {
                                    if(expression[i] == '(')//右左括号不能相接，其他情况下若前一位是)那么本位可以是任何符号
                                    {
                                        correctinput = 0;
                                        break;
                                    }
                                } 
                                else if(expression[i] == '(')//如果本位是(,那么前一位可以是除了）外任何符号
                                {
                                    lkuo++;
                                }
                                else//如果本位不是（而且前一位也不是），则两个符号相连必定出错
                                {
                                    correctinput = 0;
                                    break;
                                }
                            }
                            else if(expression[i] == ')') { rkuo++; }//whatever the character before is，rkuo or lkuo should be record correctly
                            else if(expression[i] == '(') { lkuo++; }
                            flag = 0;
                        }
                    }
                    else
                    {
                        correctinput = 0;
                    }
                }
                if(lkuo != rkuo)//if'('is not the same number as ')',the input must wrong
                {
                    correctinput = 0;
                }
                if (number < 4)//if the numbers in the input is less than 4,input must be wrong 
                {
                    correctinput = 0;
                }
                if (correctinput == 0)//when the input is incorrect, pop up a dialog to inform the user
                {
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "WRONG INPUT",
                        Content = "Check your expression.",
                        CloseButtonText = "Ok"
                    };
                    ContentDialogResult result = await noWifiDialog.ShowAsync();//to make the main thread synchronize with the dialog
                    return;
                }
                else
                {
                    if (expression[0] == '+')//after judge the input, copy the string into expression
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
                array = new int[length];
                flag = 0;
                int j = 0;
                for (int i = 0; i < length; i++,j++)//flag represent how many number elements are before this element
                {
                    if(reverse_polish_notation[i] > '9' || reverse_polish_notation[i] < '0')
                    {
                        array[j] = (int)ReversePolishNotation[i];
                        flag = 0;
                    }
                    else
                    {
                        j -= flag;
                        array[j] = (reverse_polish_notation[i] - 48)+array[j]*10;
                        flag = 1;
                    }
                }
                rpn = new int[array.Length];
                int temp;
                j = 0;
                for (int i =0; i < array.Length && array[i] != 0; i++)
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
                            while (stack.Count != 0 && stack.Peek() != '(')
                            {
                                temp = stack.Pop();
                                rpn[j++] = temp;
                            }
                            if (stack.Count != 0)
                            {
                                stack.Pop();
                            }
                        }
                    }
                } 
                while (stack.Count != 0)
                {
                    temp = stack.Pop();
                    rpn[j++] = temp;
                }
                //计算后缀表达式的值
                Stack<double> dstack = new Stack<double>();
                length = rpn.Length;
                double num1, num2, num3 = -1;
                for (int i = 0; i < length; i++)
                {
                    if(rpn[i] == 0)
                    {
                        break;
                    }
                    if (rpn[i] > 0 && rpn[i] < 14)
                    {
                        dstack.Push(rpn[i]);
                    }
                    else
                    {
                        num2 = dstack.Pop();
                        num1 = dstack.Pop();
                        if (rpn[i] == '+')
                        {
                            num3 = num1 + num2;
                        }
                        else if (rpn[i] == '-')
                        {
                            num3 = num1 - num2;
                        }
                        else if (rpn[i] == '*')
                        {
                            num3 = num1 * num2;
                        }
                        else if (rpn[i] == '/')
                        {
                            num3 = num1 / num2;
                        }
                        else if (rpn[i] == '^')
                        {
                            num3 = Math.Pow(num1,num2);
                        }
                        dstack.Push(num3);
                    }
                }
                CaculationResult = dstack.Pop();
            }
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
          //this.reverse_polish_notation = "he\nllo.";
        }
    }
}
