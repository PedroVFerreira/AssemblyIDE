using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Log //: ILog
    {
        public void AddLog(string line, int space)
        {
            string spaceStr = " ";
            for (int i = 0; i < space; i++)
                spaceStr = spaceStr + "    ";
            if (space == 0)
                Console.WriteLine();
            Console.WriteLine(spaceStr + line);
        }
    }
}
