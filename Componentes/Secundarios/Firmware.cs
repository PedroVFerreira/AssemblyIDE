using Componentes.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Componentes.Secundarios
{
    public static class Firmware
    {
        private static List<string> codigo = new List<string>
        {
            // CICLO DE BUSCA IR  IR              - inicio 0
            ConverterParaInstrucao(0, new List<int>{2 , 25, 17, 50 }, 1),
            ConverterParaInstrucao(1, new List<int>{28, 24, 18}, 2 ),
            ConverterParaInstrucao(2, new List<int>{4 , 11 }, 3),
            
            // NOP - SOMENTA PARA INICIAR CICLO DE EXECUCAO
            ConverterParaInstrucao(3, new List<int>{31}, 4),
            
            // NOP - SOMENTA PARA INICIAR CICLO DE EXECUCAO
            ConverterParaInstrucao(4, new List<int>{30}, 0),
            
            // CICLO DE BUSCA P1
            ConverterParaInstrucao(5, new List<int>{2 , 25 ,  17, 50}, 6),
            ConverterParaInstrucao(6, new List<int>{28, 24, 18}, 7 ),
            ConverterParaInstrucao(7, new List<int>{4 , 15 }, 4 ),

            // CICLO DE BUSCA P2
            ConverterParaInstrucao(8, new List<int>{2 , 25 , 17, 50 }, 9),
            ConverterParaInstrucao(9, new List<int>{28, 24, 18}, 10),
            ConverterParaInstrucao(10, new List<int>{4 , 13 }, 4),

            
            // CICLO DE BUSCA P1 E P2
            ConverterParaInstrucao(22, new List<int>{2 , 25 ,  17, 50}, 23),
            ConverterParaInstrucao(23, new List<int>{28, 24, 18}, 24 ),
            ConverterParaInstrucao(24, new List<int>{4 , 15 }, 25 ),
            ConverterParaInstrucao(25, new List<int>{2 , 25 , 17, 50 }, 26),
            ConverterParaInstrucao(26, new List<int>{28, 24, 18}, 27),
            ConverterParaInstrucao(27, new List<int>{4 , 13 }, 4),
                
            // mov ax,bx                - inicio 3
            ConverterParaInstrucao(11, new List<int>{26, 27}, 0),

            // mov ax,NUMERO                - inicio 3
            ConverterParaInstrucao(12, new List<int>{26, 12}, 0),

            // mov ax,[NUMERO]
            ConverterParaInstrucao(13, new List<int>{12 , 2 , }, 14),
            ConverterParaInstrucao(14, new List<int>{28}, 15),
            ConverterParaInstrucao(15, new List<int>{26, 4}, 0),

            // mov ax,[bx]
            ConverterParaInstrucao(16, new List<int>{27 , 2 , }, 17),
            ConverterParaInstrucao(17, new List<int>{28}, 18),
            ConverterParaInstrucao(18, new List<int>{26, 4}, 0),

            // mov [NUMERO],ax
            ConverterParaInstrucao(19, new List<int>{14 , 2 , }, 20),
            ConverterParaInstrucao(20, new List<int>{27 , 3 , }, 21),
            ConverterParaInstrucao(21, new List<int>{29}, 0),

            // mov [NUMERO],NUMERO
            ConverterParaInstrucao(22, new List<int>{14 , 2 , }, 20),
            ConverterParaInstrucao(23, new List<int>{12 , 3 , }, 21),
            ConverterParaInstrucao(24, new List<int>{29}, 0),

            //
        };

        public static string getInstrucao(int endereco)
        {
            foreach (var item in codigo)
            {
                if (CalculadoraBinario.BinarioParaInt(item.Substring(41, 9)) == endereco)
                    return item;
            }
            return null;
        }

        public static string ConverterParaInstrucao(int line, List<int> portas, int nextLine)
        {
            string r = "";
            for (int i = 0; i < 32; i++)
            {
                r += portas.Contains(i) ? "1" : "0";
            }
            var next = CalculadoraBinario.IntParaBinario(nextLine.ToString(), 9);
            foreach (var item in next)
            {
                r += item;
            }
            var address = CalculadoraBinario.IntParaBinario(line.ToString(), 9);
            foreach (var item in address)
            {
                r += item;
            }
            for (int i = 50; i < 53; i++)
            {
                r += portas.Contains(i) ? "1" : "0";
            }
            return r;
        }
    }
}
