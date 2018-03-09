using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nsRest
{
    class Rest
    {
        public string dbConnection(string connString)
        {
            return connString + "; Jet OLEDB:Database Password=zlom";
        }
    }// class Rest
}//namespace nsRest
