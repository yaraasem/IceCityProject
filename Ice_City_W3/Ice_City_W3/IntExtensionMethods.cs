using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_City_W3
{
   
    
    public static class IntExtensionMethods
    {
        public static bool IsEven(this int value) => value % 2 == 0;
        public static bool IsOdd(this int value) => value % 2 != 0;
    }
}

