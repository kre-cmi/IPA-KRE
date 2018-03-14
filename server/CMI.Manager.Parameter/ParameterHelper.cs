using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMI.Manager.Parameter
{
    public static class ParameterHelper
    {
        private static string paramaters = string.Empty;
        public static string Parameters
        {
            get => paramaters;
            set
            {
                if (value == string.Empty)
                    paramaters = string.Empty;
                else
                    paramaters += value;
            }
        }
    }
}
