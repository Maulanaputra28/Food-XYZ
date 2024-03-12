using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_XYZ.Helper
{
    public static class Validation
    {
        public static bool ValidationStirng(List<string> strgs)
        {
            foreach (string str in strgs)
            {
                if (String.IsNullOrEmpty(str))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
