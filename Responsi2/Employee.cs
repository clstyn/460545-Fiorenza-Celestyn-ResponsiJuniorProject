using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Responsi2
{
    public class Employee
    {
        private char _id;
        private string _name;
        private int _id_dep;

        public char id
        {
            get { return _id;  }
            set { _id = value; }
        }
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int id_dep
        {
            get { return _id_dep; }
            set
            {
                _id_dep = value;
            }
        }

        public static Employee clicked;
    }
}
