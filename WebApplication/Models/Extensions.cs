using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public static class Extensions
    {
        public static string ToHex(this string s, int i)
        {
          return Convert.ToInt32(s, 2).ToString("X").PadLeft(i, '0');
        }
    }
}
