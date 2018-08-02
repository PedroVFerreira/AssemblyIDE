using System;
using System.Collections.Generic;
using System.Text;

namespace Componentes.Secundarios
{
    public class Comando
    {
        public string Opcode { get; protected set; }
        public string P1 { get; protected set; }
        public string P1Info
        {
            get
            {
                return Opcode.Substring(4, 2);
            }
        }
        public string P1Valor
        {
            get
            {
                if (String.IsNullOrEmpty(P1)) return Opcode.Substring(6, 4);
                return P1;
            }
        }

        public string P2 { get; protected set; }
        public string P2Info { get
            {
                return Opcode.Substring(10, 2);
            }
        }
        public string P2Valor
        {
            get
            {
                if (String.IsNullOrEmpty(P2)) return Opcode.Substring(12, 4);
                return P2;
            }
        }

        public Comando(string opcode, string p1)
        {
            Opcode = opcode;
            P1 = p1;
            P2 = null;
        }

        public Comando(string opcode, string p1, string p2)
        {
            Opcode = opcode;
            P1 = p1;
            P2 = p2;
        }
        
    }
}
