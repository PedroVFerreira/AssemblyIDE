using Componentes.Principais;
using System;
using System.Collections.Generic;
using System.Text;

namespace Componentes.Secundarios
{
    public class Conector
    {

        public bool Aberto { get; set; }
        /// <summary>
        /// True para conector de entrada e 0 para contector de saída
        /// </summary>
        public bool Entrada { get; protected set; }
        public int NumeroConector { get; protected set; }
        public IComponente componente { get; set; }

        public Conector(int numConector, bool conectorEntrada, IComponente componente)
        {
            NumeroConector = numConector;
            this.componente = componente;   
            Entrada = conectorEntrada;
        }

        internal void AtualizaConteudo(string conteudo)
        {
            componente.setConteudo(conteudo);
        }
    }
}
