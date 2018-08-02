using Componentes.Helpers;
using Componentes.Secundarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Componentes.Principais
{
    public class Registrador : IComponente
    {
        protected string _conteudo;
        private string _name;
        public Registrador(string conteudo, string name)
        {
            _conteudo = conteudo;
            _name = name;
        }
        public Registrador()
        {
            _conteudo = "";
        }

        public void setConteudo(string conteudo)
        {
            _conteudo = conteudo;
        }

        public void AddUnidade()
        {
            _conteudo = CalculadoraBinario.Add(getConteudo(), "1");
        }

        public string getConteudo()
        {
            if (String.IsNullOrEmpty(_conteudo)) return null;
            if(_conteudo.Length < Palavras.LeitorPalavra.TamanhoPalavra)
            {
                return _conteudo.PadLeft(Palavras.LeitorPalavra.TamanhoPalavra, '0');
            }
            return _conteudo;
        }

        public string getName()
        {
            return _name;
        }
    }
}
