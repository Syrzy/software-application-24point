using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace software_application_24point
{
    class UsersCollection//ObservableCollection集合类，用于未来添加多用户显示功能时实时反馈用户数据变化
    {
        public static ObservableCollection<User> Users = new ObservableCollection<User>();//ObservableCollection将在集合内容变化时提供反馈
    }
    class User : INotifyPropertyChanged//INotifyPropertyChanged接口用于通知绑定控件绑定数值发生变化
    {
        public event PropertyChangedEventHandler PropertyChanged;//declare the interface
        private int wintimes;//means the number of this user's total winning times
        private string name;//user name
        private int losetimes;//number of total losing
        public int Losetimes//define a property to represent losetimes
        {
            get { return losetimes; }//get property value
            set 
            { 
                losetimes = value;
                if (this.PropertyChanged != null)//define PropertyChangedEventHandler interface
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Losetimes"));//PropertyChangedEventArgs will give property name
                }
            }
        }
        public int Wintimes//define a property
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
        public User(int wintimes,int losetimes,string name)//constructor
        {
            this.wintimes = wintimes;
            this.name = name;
            this.losetimes = losetimes;
        }
    }
}
