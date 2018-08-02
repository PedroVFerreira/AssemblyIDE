
using Componentes.Secundarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Componentes.Helpers
{
    public static class Compilador
    {
        public static Compilado Compilar(List<string> Font)
        {
            var restultado = new Compilado();
            var codigoXBinario = new Dictionary<int, int>();
            int _countCodigo = 0;
            int _countBinario = 0;

            var codigoBinario = new List<string>();
            foreach (var item in Font)
            {
                if (String.IsNullOrEmpty(item)) continue;
                var comando = item.Split(" ");
                if (!ComandoExiste(comando[0]))
                {
                    restultado.Erros.Add($"'{comando[0]}' não reconhecido como um comando. (linha {_countCodigo + 1})");
                    continue;
                }
                var opcode = "";
                if (comando[0] == "mov") opcode = Palavras.Opcode.Mov;
                if (comando[0] == "add") opcode = Palavras.Opcode.Add;
                if (comando[0] == "sub") opcode = Palavras.Opcode.Sub;
                if (comando[0] == "mul") opcode = Palavras.Opcode.Mul;
                if (comando[0] == "div") opcode = Palavras.Opcode.Div;
                if (comando[0] == "cmp") opcode = Palavras.Opcode.Cmp;
                var param1 = comando[1].Split(",")[0];
                var param2 = comando[1].Split(",")[1];
                var codigoCompilado = ParametroParaBinario(opcode, param1, param2);
                foreach (var linha in codigoCompilado)
                {
                    if (linha != null)
                    {
                        codigoXBinario[_countBinario] = _countCodigo;
                        codigoBinario.Add(linha);
                        _countBinario++;
                    }
                }
                _countCodigo++;
            }
            restultado.Codigo = codigoBinario;
            restultado.CodigoXBinario = codigoXBinario;
            return restultado;
        }

        private static bool ComandoExiste(string v)
        {
            var comandos = new List<string>
            {
                "mov",
                "add",
                "sub",
                "mul",
                "div",
                "cmp"
            };

            return comandos.Contains(v);
        }

        public static string ComponenteParaBinario(string nome, bool escrita)
        {
            if (nome == "ax")
                return escrita ? IntParaBinario(Portas.Ax.Entrada): IntParaBinario(Portas.Ax.Saida);
            if (nome == "bx")
                return escrita ? IntParaBinario(Portas.Bx.Entrada) : IntParaBinario(Portas.Bx.Saida);
            if (nome == "cx")
                return escrita ? IntParaBinario(Portas.Cx.Entrada) : IntParaBinario(Portas.Cx.Saida);
            if (nome == "dx")
                return escrita ? IntParaBinario(Portas.Dx.Entrada) : IntParaBinario(Portas.Dx.Saida);
            return null;
        }

        public static string IntParaBinario(int i)
        {
            return Convert.ToString(i, 2).PadLeft(4, '0');
        }

        public static string[] ParametroParaBinario(string opcode, string p1, string p2)
        {
            var resposta = new string[3];
            string instrucao1 = opcode;
            string instrucao2 = null;
            string instrucao3 = null;
            p1 = p1.Replace(" ", "");
            p2 = p2.Replace(" ", "");
            if (p1.StartsWith("["))
            {
                if (EhHexa(p1.Replace("[", "").Replace("]", "")))
                {
                    instrucao1 += Palavras.Param.IndiretoNumero + "0000";
                    instrucao2 = HexParaBinario(p1.Replace("[", "").Replace("]", ""), 16);
                }
                else
                {
                    instrucao1 += Palavras.Param.IndiretoRegistrador + ComponenteParaBinario(p1.Replace("[", "").Replace("]", ""), false);
                }
            }
            else
            {
                instrucao1 += Palavras.Param.DiretoRegistrador + ComponenteParaBinario(p1, true);
            }

            if (p2.StartsWith("["))
            {
                if (EhHexa(p2.Replace("[","").Replace("]", "")))
                {
                    instrucao1 += Palavras.Param.IndiretoNumero + "0000";
                    if(instrucao2 == null)
                        instrucao2 = HexParaBinario(p2.Replace("[", "").Replace("]", ""), 16);
                    else
                        instrucao3 = HexParaBinario(p2.Replace("[", "").Replace("]", ""), 16);

                }
                else
                {
                    instrucao1 += Palavras.Param.IndiretoRegistrador + ComponenteParaBinario(p2.Replace("[", "").Replace("]", ""), false);

                }
            }
            else
            {
                if (EhHexa(p2))
                {
                    instrucao1 += Palavras.Param.DiretoNumero + "0000";

                    if (instrucao2 == null)
                        instrucao2 =HexParaBinario(p2, 16);
                    else
                        instrucao3 = HexParaBinario(p2, 16);
                }
                else
                {
                    instrucao1 += Palavras.Param.DiretoRegistrador + ComponenteParaBinario(p2, false);

                }
            }
            resposta[0] = instrucao1;
            resposta[1] = instrucao2;
            resposta[2] = instrucao3;
            return resposta;
        }

        private static string HexParaBinario(string v1, int v2)
        {
            return CalculadoraBinario.HexParaBinario(v1, v2);
        }

        public static bool EhHexa(string n)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(n, @"\A\b[0-9a-fA-F]+\b\Z");
        }
    }

    public class Compilado
    {
        public Dictionary<int, int> CodigoXBinario { get; set; } = new Dictionary<int, int>();
        public List<string> Codigo { get; set; } = new List<string>();
        public List<string> Erros { get; set; } = new List<string>();
    }
}
