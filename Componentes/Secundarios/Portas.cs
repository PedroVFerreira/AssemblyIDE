using System;
using System.Collections.Generic;
using System.Text;

namespace Componentes.Secundarios
{
    public static class Portas
    {
        public static class PC
        {
            public static int Entrada = 25;
            public static int Saida = 26;
        }
        public static class Mar
        {
            public static int Entrada = 3;
        }
        public static class Mbr
        {
            public static int Entrada = 4;
            public static int Saida = 5;
        }
        public static class Ax
        {
            public static int Entrada = 6;
            public static int Saida = 7;
        }
        public static class Bx
        {
            public static int Entrada = 8;
            public static int Saida = 9;
        }
        public static class Cx
        {
            public static int Entrada = 10;
            public static int Saida = 11;
        }
        public static class Dx
        {
            public static int Entrada = 1;
            public static int Saida = 2;
        }
        public static class Ir
        {
            public static int Entrada = 12;
            public static int EntradaP1 = 16;
            public static int EntradaP2 = 14;
            public static int SaidaP1 = 15;
            public static int SaidaP2 = 13;
        }

        public static class Ula
        {
            public static int EntradaUla = 18;
            public static int EntradaX = 17;
            public static int SaidaAc = 19;
        }
    }
}
