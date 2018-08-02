using Componentes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Componentes.Secundarios
{
    public class Barramento : ICiclo
    {
        public List<Conector> Conectores { get; set; } = new List<Conector>();
        private ILog _logger;

        public Barramento(ILog logger)
        {
            _logger = logger;
        }

        public void AddConector(Conector conector)
        {
            Conectores.Add(conector);
        }

        private void FechaTodosConectores()
        {
            foreach (var item in Conectores)
            {
                item.Aberto = false;
            }
        }

        private void AbreConector(int conectorNum)
        {
            var conector = GetConector(conectorNum);

            if (conector != null)
                conector.Aberto = true;
        }
        private void FechaConector(int conectorNum)
        {
            var conector = GetConector(conectorNum);

            if(conector != null)
                conector.Aberto = false;
        }

        private Conector GetConector(int v)
        {
            foreach (var conector in Conectores)
            {
                if (v == conector.NumeroConector)
                    return conector;
            }
            return null;
        }

        private void FechaConectores()
        {
            foreach (var item in Conectores)
            {
                item.Aberto = false;
            }
        }

        public void Transmite()
        {
           // Procura o primeiro conector de saida que esta aberto
           var conectorDeSaida = Conectores.FirstOrDefault(c => c.Aberto && !c.Entrada);

           if (conectorDeSaida == null) return;
            // conteudo que está sendo transmitido
            var conteudo = conectorDeSaida.componente.getConteudo();
           // Percorre cada conectro de Entrada que esteja aberto
           // E manda o sinal
            foreach (var conector in Conectores)
            {
                if (conector.Aberto && conector.Entrada)
                {
                    conector.AtualizaConteudo(conteudo);
                    var log = String.Format("{0} <- {1}  {2},{3}",
                                             conector.componente.getName(),
                                             conectorDeSaida.componente.getName(),
                                             conectorDeSaida.NumeroConector,
                                             conector.NumeroConector).PadRight(40, ' ');
                    _logger.AddLog(log + conteudo);
                }
            }
        }

        public void Ciclo(string instrucao)
        {
            FechaConectores();
            for (int i = 0; i < 26; i++)
            {
                if (instrucao[i] == '1')
                    AbreConector(i + 1);
            }
            Transmite();
        }
    }
}
