using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace software_application_24point
{
    class Solve : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int a1, a2, a3, a4;
        public void ProduceRandomNumber()
        {
            Random rd = new Random();
            A1 = ""+((rd.Next(1, 1000)) % 13 + 1);
            A2 = (rd.Next(1, 1000)) % 13 + 1;
            A3 = (rd.Next(1, 1000)) % 13 + 1;
            A4 = (rd.Next(1, 1000)) % 13 + 1;
        }
        public string A1
        {
            get { return ""+a1; }
            set
            {
                a1 = Convert.ToInt32(value);
                if (this.PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("a1"));
                }
            }
        }
        public int A2
        {
            get { return a2; }
            set
            {
                a2 = value;
                if (this.PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("a2"));
                }
            }
        }
        public int A3
        {
            get { return a3; }
            set
            {
                a3 = value;
                if (this.PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("a3"));
                }
            }
        }
        public int A4
        {
            get { return a4; }
            set
            {
                a4 = value;
                if (this.PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("a4"));
                }
            }
        }
    }
}
