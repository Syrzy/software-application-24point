using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace software_application_24point
{
    class UsersCollection
    {
        public static ObservableCollection<User> Users = new ObservableCollection<User>();
    }
    class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int wintimes;
        private string name;
        private int losetimes;
        public int Losetimes
        {
            get { return losetimes; }
            set 
            { 
                losetimes = value;
                if (this.PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Losetimes"));
                }
            }
        }
        public int Wintimes
        {
            get { return wintimes; }
            set
            {
                wintimes = value;
                if (this.PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Wintimes"));
                }
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (this.PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }
        public User(int wintimes,int losetimes,string name)
        {
            this.wintimes = wintimes;
            this.name = name;
            this.losetimes = losetimes;
        }
    }
}
