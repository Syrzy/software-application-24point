using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace software_application_24point
{
    class User
    {
        private int points;
        private int wintimes;
        private string name;
        int Points
        {
            get { return points; }
            set { points = value; }
        }
        int Wintimes
        {
            get { return wintimes; }
            set { wintimes = value; }
        }
        string Name
        {
            get { return name; }
            set { name = value; }
        }
        /*User()
        {
            Windows.Storage.ApplicationDataCompositeValue composite = new Windows.Storage.ApplicationDataCompositeValue();

            composite["intVal"] = 1;
            composite["strVal"] = "string";

            localSettings.Values["exampleCompositeSetting"] = composite;

            // Retrieve composite setting
            Windows.Storage.ApplicationDataCompositeValue compositeValue =
                (Windows.Storage.ApplicationDataCompositeValue)localSettings.Values["exampleCompositeSetting"];

            if (compositeValue != null)
            {
                int intVal = (int)compositeValue["intVal"];

                string strVal = (string)compositeValue["strVal"];
            }
        }*/
    }
}
