using System;
using System.Collections.Generic;
using System.Text;

namespace Componentes.Helpers
{
    static class CalculadoraBinario
    {
        public static string Add(string numA , string numB)
        {
            int numberoA = Convert.ToInt32(numA, 2);
            int numeroB = Convert.ToInt32(numB, 2);

            return Convert.ToString(numberoA + numeroB, 2);
        }

        public static int BinarioParaInt(string i)
        {
            return Convert.ToInt32(String.IsNullOrEmpty(i) ? "0" : i, 2);
        }

        public static string IntParaBinario(string numeroStr, int tamanho)
        {
            int numero = 0;
            Int32.TryParse(numeroStr, out numero);
            return Convert.ToString(numero, 2).PadLeft(tamanho, '0');
        }

        public static string HexParaBinario(string hexValue, int tamanho)
        {
            var number = UInt64.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);

            return IntParaBinario(number.ToString(), tamanho); 
        }
    }
}
