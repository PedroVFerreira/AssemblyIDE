using Componentes.Secundarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Componentes.Helpers
{
    public static class Decodificador
    {
        public static int DecodificaPorta(string param)
        {
            return CalculadoraBinario.BinarioParaInt(param) - 1;
        }

        internal static int DecodificaInstrucaoIndirecao(Comando comando)
        {
            if ((comando.P1Info == Palavras.Param.DiretoNumero ||
               comando.P1Info == Palavras.Param.IndiretoNumero) &&
               (comando.P2Info == Palavras.Param.DiretoNumero ||
                comando.P2Info == Palavras.Param.IndiretoNumero))
                return 23;
                if (comando.P1Info == Palavras.Param.DiretoNumero ||
                comando.P1Info == Palavras.Param.IndiretoNumero)
                return 5;
            if (comando.P2Info == Palavras.Param.DiretoNumero ||
                comando.P2Info == Palavras.Param.IndiretoNumero)
                return 8;
            return 3;
        }
        internal static int DecodificaInstrucaoExecucao(Comando comando)
        {
            if(comando.Opcode.Substring(0,4) == Palavras.Opcode.Mov)
            {
                if(comando.P1Info == Palavras.Param.DiretoRegistrador)
                {
                    if(comando.P2Info == Palavras.Param.DiretoRegistrador)
                    {
                        return 11;
                    }
                    if (comando.P2Info == Palavras.Param.DiretoNumero)
                    {
                        return 12;
                    }
                    if (comando.P2Info == Palavras.Param.IndiretoNumero)
                    {
                        return 13;
                    }
                    if (comando.P2Info == Palavras.Param.IndiretoRegistrador)
                    {
                        return 16;
                    }
                }
                if(comando.P1Info == Palavras.Param.IndiretoNumero)
                {
                    if(comando.P2Info == Palavras.Param.DiretoRegistrador)
                    {
                        return 19;
                    }
                }
            }

            return 0;
        }
    }
}
