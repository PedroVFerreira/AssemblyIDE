using System;
using System.Collections.Generic;
using System.Text;

namespace Componentes.Helpers
{
    public static class Extensions
    {
        public static string ChangeCaracter(this string s, int i, char v)
        {
            var r = "";
            var index = 0;
            foreach (var item in s)
            {
                if (index == i)
                    r += v;
                else
                    r += item;
                index++;
            }
            return r;
        }
    }
}
