using System;
using System.Collections.Generic;
using System.Text;

namespace Componentes.Secundarios
{
    public class Flags
    {
        public Flag flagZero { get; set; } = new Flag(false, "ZF: Flag igual a zero");
        public Flag flagSinal { get; set; } = new Flag(false, "SF: Flag de sinal");
        public Flag flagOverflow { get; set; } = new Flag(false, "OF: Flag de overflow");
    }
}
