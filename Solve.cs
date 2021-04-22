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
    class Solve : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ArrayList Rpnlist;//Rpnlist is a collection of RPN,used to record the potential RPN so as to find the repetition
        public bool Correct;
        private int[] Nlist;//number list, represent the 4 numbers for 24 caculation
        private int[] Nlist2;//number list, the copy of Nlist, whitch is used in finding solution function
        private char[] Olist;//operation list
        private int[] Rpn;// reverse polish notation
        private string allsolution;//represents the all the solutions for certain Nlist
        public string AllSolution
        {
            get { return allsolution; }
            set 
            {
                allsolution = value;
                if (this.PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("AllSolution"));
                }
            }
        }
        public void ProduceRandomNumber()//produce random numbers for player
        {
            Random rd = new Random();
            A1 = ((rd.Next(1, 1000)) % 13 + 1).ToString();
            A2 = (rd.Next(1, 1000)) % 13 + 1;
            A3 = (rd.Next(1, 1000)) % 13 + 1;
            A4 = (rd.Next(1, 1000)) % 13 + 1;
            for (int i = 0; i < 4; i++)
            {
                Nlist2[i] = Nlist[i];//copy Nlist into Nlist2.这里也可以使用Copy功能
            }
            FindAllSolution();//test if these numbers have at least one solution
            if (AllSolution == "")//means there is no solution
            {
                ProduceRandomNumber();//produce again
            }
            AllSolution = "";//because the AllSolution property is binding with the text of textblock control,so after the test AllSolution need to be clean.
        }
        public string A1
        {
            get { return Nlist[0].ToString(); }
            set
            {
                Nlist[0] = Convert.ToInt32(value);
                if (this.PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("A1"));
                }
            }
        }
        public int A2
        {
            get { return Nlist[1]; }
            set
            {
                Nlist[1] = value;
                if (this.PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("A2"));
                }
            }
        }
        public int A3
        {
            get { return Nlist[2]; }
            set
            {
                Nlist[2] = value;
                if (this.PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("A3"));
                }
            }
        }
        public int A4
        {
            get { return Nlist[3]; }
            set
            {
                Nlist[3] = value;
                if (this.PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("A4"));
                }
            }
        }
        public void FindAllSolution()//顾名思义
        {
            AllSolution = "";//initialize the output
            FindAllSolution1(0);
            Rpnlist = new ArrayList();//FindAllSolution will produce many rpn and storage them into the Rpnlist
            FindAllSolution2(0);
            Rpnlist = new ArrayList();
        }
        /*这里不好装逼用英文了。核心思路还是后缀表达式。由于4个数字三个运算符且运算符均为双元运算符，所以显然后缀表达式的形式有且仅有两种：
         数 数 符 数 符 数 符 或者 数 数 符 数  数 符 符。那么我们只需要按照固定形式向7位后缀表达式中填充数字或者符号，即可完成遍历。
        根据这两种表达形式分别进行深度为8的递归,查找所有可能的计算情况。递归思路如下：
        递归深度代表了本轮递归所填充的位置，根据深度可以选择向该位置填充数字或者符号；而数字只能是1-13，用一个新的Nlist2存储数组，如果数字已被使用，
        则将之置为0（递归返回后再改回原值），表示该数字已被使用。而符号则没有使用次数限制。当递归进行到深度为8时，后缀表达式已经被填充完毕，因此
        可以进行计算，如果等于24，则成功找到一个解*/
        private void FindAllSolution1(int i)
        {
            if(i < 7)
            {
                if (i == 0 || i == 1 || i == 3 || i == 5)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (Nlist2[j] == 0) { continue; }
                        else
                        {
                            Rpn[i] = Nlist[j];
                            Nlist2[j] = 0;
                            FindAllSolution1(1+i);
                            Nlist2[j] = Rpn[i];
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < 5; j++)
                    {
                        Rpn[i] = Olist[j];
                        FindAllSolution1(1+i);
                    }
                }
            }
            else
            {
                if (CaculateRpn(Rpn) == 24)
                {
                    if (!IsRpnRepeat(Rpn,1))
                    {
                        AllSolution += (ConvertRpn(Rpn) + '\n');
                    }
                }
            }
            return;
        }
        private void FindAllSolution2(int i)
        {
            if (i < 7)
            {
                if (i == 0 || i == 1 || i == 3 || i == 4)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (Nlist2[j] == 0) { continue; }
                        else
                        {
                            Rpn[i] = Nlist[j];
                            Nlist2[j] = 0;
                            FindAllSolution2(1 + i);
                            Nlist2[j] = Rpn[i];
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < 5; j++)
                    {
                        Rpn[i] = Olist[j];
                        FindAllSolution2(1 + i);
                    }
                }
            }
            else
            {
                if (CaculateRpn(Rpn) == 24)
                {
                    if (!IsRpnRepeat(Rpn, 2))
                    {
                        AllSolution += (ConvertRpn(Rpn) + '\n');
                    }
                }
            }
            return;
        }
        /*这里同样需要大段中文来解释一下。本方法的用处是根据新后缀式rpn，该后缀式的模式以及已有解（存储在Rpnlist里）来判断新后缀式rpn是否重复
         思路如下：在4个数字，且结果为24的一系列后缀式中，如果后缀式模式一样则意味着采取相同的运算顺序，如果此时后缀式的符号种类也一样（不要求符号
        顺序相同，不要求数字顺序相同），则两个后缀式一定可以互相转化。本方法将找出新rpn的符号种类是否和已有Rpnlist中的相同，如相同则不予采纳*/
        private bool IsRpnRepeat(int[] rpn, int mode) //mode represents this function will be used in which kind of rpnlist
        {
            bool IsRepeat = false;
            int length = Rpnlist.Count;
            int[] templist = new int[7];
            for (int i = 0;i < length; i++)
            {
                templist = Rpnlist[i] as int[];
                if (mode == 1)//mode 1 represents "nnonono" style rpnlist
                {
                    int[] OperationList1 = new int[3] { templist[2], templist[4], templist[6] };
                    int[] OperationList2 = new int[3] { rpn[2], rpn[4], rpn[6] };
                    Array.Sort(OperationList1);
                    Array.Sort(OperationList2);
                    if (Enumerable.SequenceEqual(OperationList1, OperationList2))//compare the two list
                    {
                        IsRepeat = true;//if all same,very much propbably they are repeat rpn
                    }
                }
                if (mode == 2)//mode 2 represents "nnonnoo" style rpnlist
                {
                    int[] OperationList1 = new int[3] { templist[2], templist[5], templist[6] };
                    int[] OperationList2 = new int[3] { rpn[2], rpn[5], rpn[6] };
                    Array.Sort(OperationList1);
                    Array.Sort(OperationList2);
                    if (Enumerable.SequenceEqual(OperationList1, OperationList2))
                    {
                        IsRepeat = true;
                    }
                }
            }
            if (!IsRepeat)
            {
                int[] Copyrpn = new int[7];//add function is shallow copy
                rpn.CopyTo(Copyrpn, 0);
                Rpnlist.Add(Copyrpn);

            }
            return IsRepeat;
        }
        public double CaculateRpn(int[] rpn)//I think the name is very straight
        {
            double CaculationResult;
            Stack<double> stack = new Stack<double>();
            int length = rpn.Length;
            double num1, num2, num3 = -1;
            for (int i = 0; i < length; i++)
            {
                if (rpn[i] == 0)
                {
                    break;
                }
                if (rpn[i] > 0 && rpn[i] < 14)
                {
                    stack.Push(rpn[i]);
                }
                else
                {
                    num2 = stack.Pop();
                    num1 = stack.Pop();
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
                        if ((int)num3 - num3 > 0.00000001 || (int)num3 - num3 < -0.00000001)
                        {
                            stack.Push(-1);
                            break;
                        }
                        num3 = (int)Math.Pow(num1, num2);
                    }
                    stack.Push(num3);
                }
            }
            CaculationResult = stack.Pop();
            return CaculationResult;
        }
        public string ConvertRpn(int[] rpn)//this function will transform the ASCII style reverse polish notation into nifix expression of string style
        {
            Stack<string> stack = new Stack<string>();
            for (int i = 0; i < rpn.Length; i++)
            {
                //actually the progress is caculating the rpn
                int n = rpn[i];
                if (n > 0  && n < 14)
                {
                    string value = "";
                    value += n.ToString();
                    stack.Push(value);
                }
                else
                {
                    string y = stack.Pop();
                    string x = stack.Pop();
                    switch (n)
                    {
                        case '+': stack.Push('(' + x + '+' + y + ')'); break;//the core is not caculate the two number but join them in string type
                        case '-': stack.Push('(' + x + '-' + y + ')'); break;
                        case '*': stack.Push('(' + x + '*' + y + ')'); break;
                        case '/': stack.Push('(' + x + '/' + y + ')'); break;
                        case '^': stack.Push('(' + x + '^' + y + ')'); break;
                    }
                }
            }
            return stack.Pop();
        }
        public async Task Judge(double n, int[] array)
        {
            int[] Numinput = new int[4];//judge if the input used those given numbers
            int i, j;
            for (i = 0, j = 0; i < array.Length && j < 4; i++)
            {
                if (array[i] > 0 && array[i] < 14)
                {
                    Numinput[j++] = array[i];
                }
            }
            for (i = 0; i < 4; i++)
            {
                Nlist2[i] = Nlist[i];
            }
            Array.Sort(Nlist2);
            Array.Sort(Numinput);
            for (i = 0; i < 4; i++)
            {
                if (Nlist2[i] != Numinput[i])
                {
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "Used illegal number",
                        Content = "At least one number you have used is not the given number",
                        CloseButtonText = "Sad."
                    };
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    return;
                }
            }
            if (n != 24)
            {
                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "WRONG Solution",
                    Content = "Come on!",
                    CloseButtonText = "Sad."
                };
                ContentDialogResult result = await noWifiDialog.ShowAsync();
                return;
            }
            else
            {
                Correct = true;
                ProduceRandomNumber();
                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "Good Solution",
                    Content = "Well done!",
                    CloseButtonText = "Nice."
                };
                ContentDialogResult result = await noWifiDialog.ShowAsync();
                return;
            }
        }
        public Solve()
        {
            Nlist = new int[4] ;
            Nlist2 = new int[4] ;
            Olist = new char[5] {'+','-','*','/','^' };
            Rpn = new int[7];
            Correct = false;
            Rpnlist = new ArrayList();
        }
    }
}
