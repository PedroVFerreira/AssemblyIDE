using Componentes.Helpers;
using Componentes.Secundarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Componentes.Principais
{
    public class ULA : ICiclo, IComponente
    {
        public Flags Flags { get; set; }
        public Registrador AC { get; set; } = new Registrador("", "AC");
        public Registrador X { get; set; } = new Registrador("", "X");
        public string _conteudo { get; set; }
        public string _f { get; set; }
        public ULA()
        {
            Flags = new Flags();
        }

        public string Executa(string op, string a, string b)
        {
            if(op == Palavras.Opcode.Add)
            {
               return CalculadoraBinario.Add(a, b);
            }

            return null;
        }

        public void setConteudo(string conteudo)
        {
            _conteudo = conteudo;
        }

        public string getConteudo()
        {
            return _conteudo;
        }

        public string getName()
        {
            return "ULA";
        }

        public void Ciclo(string instrucao)
        {
            var instrucaoUla = instrucao[48].ToString() + instrucao[49].ToString() + instrucao[50].ToString();
            // inc
            if (instrucaoUla == "001")
            {
                AC.setConteudo(CalculadoraBinario.Add(_conteudo, "1"));
            }
        }
    }
}
