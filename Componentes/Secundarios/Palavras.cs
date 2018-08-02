using System;
using System.Collections.Generic;
using System.Text;

namespace Componentes.Secundarios
{
    public static class Palavras
    {
        public static class LeitorPalavra
        {
            public static int TamanhoPalavra = 16;
            public static Comando Ler(string Palavra)
            {
                string opcode = "";
                string p1 = "";
                string p2 = "";

                opcode = Palavra.Substring(0, 16);
               
                if (Palavra.Substring(4, 2) == Palavras.Param.DiretoNumero ||
                    Palavra.Substring(4, 2) == Palavras.Param.IndiretoNumero)
                {
                    p1 = Palavra.Substring(16, 16);
                }
                if (Palavra.Substring(10, 2) == Palavras.Param.DiretoNumero ||
                  Palavra.Substring(10, 2) == Palavras.Param.IndiretoNumero) { 
                    p2 = Palavra.Substring(32, 16);
                }
                return new Comando(opcode, p1, p2);
            }
        }

        public static class Opcode
        {
            // USO INTERNO PARA SINALIZAR QUE A PAVRA E UM DADO
            public static string DADO = "0000";

            public static string Mov = "0001";
            public static string Inc = "0010";
            public static string Add = "0011";
            public static string Sub = "0100";
            public static string Mul = "0101";
            public static string Div = "0110";
            public static string Cmp = "0111";
            public static string Je  = "1000";
            public static string Jne = "1001";
            public static string Jg  = "1010";
            public static string Jge = "1011";
            public static string Jl  = "1100";
            public static string Jle = "1101";

        }

        public static class Param
        {
            public static string IndiretoRegistrador = "10";
            public static string IndiretoNumero = "11";
            public static string DiretoNumero = "00";
            public static string DiretoRegistrador = "01";
        }
    }
}
