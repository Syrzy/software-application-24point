using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace software_application_24point
{
    class UsersCollection
    {
        public static ObservableCollection<User> Users = new ObservableCollection<User>();
    }
    class User
    {
        private int totalwintimes;
        private int wintimes;
        private string name;
        public int Totalwintimes
        {
            get { return totalwintimes; }
            set { totalwintimes = value; }
        }
        public int Wintimes
        {
            get { return wintimes; }
            set { wintimes = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public User(int totalwintimes,int wintimes,string name)
        {
            this.totalwintimes = totalwintimes;
            this.wintimes = wintimes;
            this.name = name;
        }
    }
}
