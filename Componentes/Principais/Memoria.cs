using Componentes.Helpers;
using Componentes.Secundarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Componentes.Principais
{
    public class Memoria  : ICiclo
    {
        private List<Dado> Conteudo { get; set; } = new List<Dado>();
        private ILog _logger;
        private Registrador Mar;
        private Registrador Mbr;

        private string enderecoAtual = "";
        public int contador = 0;
        private string Estado = "Leitura";
        public Memoria(Registrador mar, Registrador mbr, ILog logger)
        {
            Mar = mar;
            Mbr = mbr;
            _logger = logger;

            Conteudo.Add(new Dado
            {
                Conteudo = "101",
                Endereco = 256
            });
        }

        public List<Dado> getMemoriaToda()
        {
            return Conteudo.OrderBy(c => c.Endereco).ToList();
        }

        private string getMemoria(string endereco)
        {
            var enderecoInt = CalculadoraBinario.BinarioParaInt(endereco);
            

            var dado = Conteudo.Where(c => c.Endereco == enderecoInt).FirstOrDefault();
            if (dado == null) return "".PadLeft(Palavras.LeitorPalavra.TamanhoPalavra, '0');
            return dado.Conteudo;

        }

        private void AddOuEditaMemoria(string endereco, string conteudo)
        {
            var enderecoInt = CalculadoraBinario.BinarioParaInt(endereco);

            var dado = Conteudo.Where(c => c.Endereco == enderecoInt).FirstOrDefault();
            if (dado == null) {
                Conteudo.Add(new Dado
                {
                    Conteudo = conteudo,
                    Endereco = enderecoInt
                });
            }
            else dado.Conteudo = conteudo;

        }

        public void AddCodigo(string conteudo)
        {
            Conteudo.Add(new Dado
            {
                Conteudo = conteudo,
                Endereco = contador
            });
            contador++;
        }

        public string getConteudo()
        {
            return getMemoria(enderecoAtual);
        }


        public void setEstadoLeitura()
        {
            Estado = "Leitura";
        }
        public void setEstadoEscrita()
        {
            Estado = "Escrita";
        }

        public string getName()
        {
            return "Memoria";
        }

        internal void ExecutaMemoria()
        {
            enderecoAtual = Mar.getConteudo();
            if (Estado == "Leitura")
            {
                _logger.AddLog("MBR <- MEMORIA");
                Mbr.setConteudo(getConteudo());
            }
            else if(Estado == "Escrita")
                AddOuEditaMemoria(enderecoAtual, Mbr.getConteudo());
        }

        public void Ciclo(string codigoFirmware)
        {
            if (codigoFirmware[29] == '1')
            {
                setEstadoEscrita();
            }
            else if(codigoFirmware[28] == '1')
            {
                setEstadoLeitura();
            }
            else
            {
                setEstadoNeutro();
            }

            ExecutaMemoria();
        }

        private void setEstadoNeutro()
        {
            Estado = "Neutro";
        }

    }

    public class Dado
    {
        public int Endereco { get; set; }
        public string Conteudo { get; set; }
    }
}
