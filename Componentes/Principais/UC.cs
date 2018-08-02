using Componentes.Helpers;
using Componentes.Secundarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Componentes.Principais
{
    public class UC
    {
        public Registradores Registradores { get; set; }
        public Memoria Memoria { get; set; }
        public ULA Ula { get; set; }
        public Barramento BarramentoA { get; set; }
        public Barramento BarramentoB { get; set; }
        private ILog _logger;
        private int _iccOld = 0;
        private int _icc = 0;
        private bool instrucaoRodou = false;
        private Dictionary<int, int> _linhaBinarioXCodigo = new Dictionary<int, int>();


        private int _enderecoFirmware = 0;
        private readonly IR IR;

        public UC(ILog logger, Dictionary<int, int> linhaBinarioXCodigo)
        {
            logger = logger == null ? new Logger() : logger;
            _logger = logger;
            _linhaBinarioXCodigo = linhaBinarioXCodigo;

            Registradores = new Registradores();
            BarramentoA = new Barramento(logger);
            BarramentoB = new Barramento(logger);
            
            Ula = new ULA();

            ConectaConectoresAoComponente(Portas.Ula.EntradaUla, -1, BarramentoA, Ula);
            ConectaConectoresAoComponente(Portas.Ula.EntradaX, -1, BarramentoA, Ula.X);
            ConectaConectoresAoComponente(-1, Portas.Ula.SaidaAc, BarramentoA, Ula.AC);

            Registradores.PC  = ConfiguraRegistrador( Portas.PC.Entrada, Portas.PC.Saida,  "0", "PC",  BarramentoA);
            Registradores.MAR = ConfiguraRegistrador( Portas.Mar.Entrada, -1, null, "MAR", BarramentoA);
            Registradores.MBR = ConfiguraRegistrador( Portas.Mbr.Entrada, Portas.Mbr.Saida, null, "MBR", BarramentoA);
            Registradores.AX  = ConfiguraRegistrador(Portas.Ax.Entrada, Portas.Ax.Saida, null,  "AX", BarramentoA);
            Registradores.BX  = ConfiguraRegistrador(Portas.Bx.Entrada, Portas.Bx.Saida, null,  "BX", BarramentoA);
            Registradores.CX  = ConfiguraRegistrador(Portas.Cx.Entrada, Portas.Cx.Saida, null, "CX", BarramentoA);
            Registradores.DX  = ConfiguraRegistrador(Portas.Dx.Entrada, Portas.Dx.Saida, null, "DX", BarramentoA);

            Registradores.IR = ConfiguraIr(Portas.Ir.Entrada, -1, BarramentoA);
            IR = Registradores.IR;
            ConectaConectoresAoComponente(Portas.Ir.EntradaP1, Portas.Ir.SaidaP1, BarramentoA, Registradores.IR.P1);
            ConectaConectoresAoComponente(Portas.Ir.EntradaP2, Portas.Ir.SaidaP2, BarramentoA, Registradores.IR.P2);

            Memoria = new Memoria(Registradores.MAR, Registradores.MBR, logger);
            
        }
        
        public int LinhaCodigoAtual()
        {
           
            var pc = CalculadoraBinario.BinarioParaInt(Registradores.PC.getConteudo()) -1;
            int r = -1;
            try
            {
                r = _linhaBinarioXCodigo[pc];
            }
            catch (Exception)
            {}
            return r;
        }

        private Registrador ConfiguraRegistrador(int numEntrada,
                                          int numSaida,
                                          string conteudo,
                                          string nome,
                                          Barramento barramento)
        {

            var registrador = new Registrador(conteudo, nome);
            // Adiciona Conectores no Barramento 

            ConectaConectoresAoComponente(numEntrada, numSaida, barramento, registrador);

            return registrador;
        }
        
        private IR ConfiguraIr(int numEntrada,
                                          int numSaida,
                                          Barramento barramento)
        {
            var registrador = new IR();
            // Adiciona Conectores no Barramento 

            ConectaConectoresAoComponente(numEntrada, numSaida, barramento, registrador);

            return registrador;
        }

        private static void ConectaConectoresAoComponente(int numEntrada, int numSaida, Barramento barramento, IComponente registrador)
        {
            // Conector de Entrada do registrador 
            var conecE = new Conector(numEntrada, true, registrador);
            // Conector de Saida do registrador 
            var conecS = new Conector(numSaida, false, registrador);

            barramento.AddConector(conecE);
            barramento.AddConector(conecS);
        }

        public void Rodar()
        {
            _logger.AddLog("----------------");
            var codigoFirmware = Firmware.getInstrucao(_enderecoFirmware);

            _icc = CalculadoraBinario.BinarioParaInt(codigoFirmware[30].ToString() + codigoFirmware[31].ToString());
            var comando = Palavras.LeitorPalavra.Ler(IR.getConteudo());

            // indirecao
            if (_icc == 1)
            {
                _enderecoFirmware = Decodificador.DecodificaInstrucaoIndirecao(comando);
                codigoFirmware = Firmware.getInstrucao(_enderecoFirmware);
            }
            // execucao
            if (_icc == 2) {                
                _enderecoFirmware = Decodificador.DecodificaInstrucaoExecucao(comando);
                codigoFirmware = Firmware.getInstrucao(_enderecoFirmware);
            }

            
            if (codigoFirmware[26] == '1')
                codigoFirmware = codigoFirmware.ChangeCaracter(Decodificador.DecodificaPorta(comando.P1Valor), '1');
            
            if (codigoFirmware[27] == '1')
                codigoFirmware = codigoFirmware.ChangeCaracter(Decodificador.DecodificaPorta(comando.P2Valor), '1');

            BarramentoA.Ciclo(codigoFirmware);
            Memoria.Ciclo(codigoFirmware);
            Ula.Ciclo(codigoFirmware);


            _enderecoFirmware = CalculadoraBinario.BinarioParaInt(codigoFirmware.Substring(32,9));
        }

        

        // -------------------------------------------

        //public void RodarInstrucao()
        //{
        //    var count = 0;
        //    instrucaoRodou = false;
        //    while (!instrucaoRodou && count < 4)
        //    {
        //        Clock();
        //        count++;
        //    }
        //}

        //public void Clock()
        //{
        //    if (_iccOld == 0)
        //        CicloDeBusca();
        //    else if (_iccOld == 1)
        //        ClicoIndireto();
        //    else if (_iccOld == 2)
        //    {
        //        ClicoExecucao();
        //        Registradores.IR.Limpa();
        //        instrucaoRodou = true;
        //    }
        //}

        //private void ClicoExecucao()
        //{
        //    _logger.AddLog("Ciclo de execucao");
        //    var comando = Palavras.LeitorPalavra.Ler(Registradores.IR.getConteudo());
        //    var p1 = Registradores.IR.P1;
        //    var p2 = Registradores.IR.P2;
        //    if (comando.Opcode == Palavras.Opcode.Mov)
        //    {

        //        if (comando.P1Info == Palavras.Param.DiretoRegistrador)
        //        {
        //            // Se for atribuição para Registrador
        //            if (comando.P2Info == Palavras.Param.DiretoRegistrador)
        //            {
        //                // Transfere entre Registradores
        //                TransfereDadosPorConectoresNoBarramento(comando.P2Valor, comando.P1Valor, BarramentoA);
        //            }
        //            else if (comando.P2Info == Palavras.Param.IndiretoNumero)
        //            {
        //                //Transfere de P2
        //                p2.atualizaConteudoParametro(p2.getConteudo().Substring(2));
        //                TransfereDadosPorConectoresNoBarramento(Portas.Ir.SaidaP2, comando.P1Valor, BarramentoA);
        //            }
        //            else if (comando.P2Info == Palavras.Param.DiretoNumero)
        //            {
        //                //Carrega o próximo valor para P2
        //                p2.atualizaConteudoParametro(null);
        //                CicloDeBusca();
        //                //Transfere de P2
        //                TransfereDadosPorConectoresNoBarramento(Portas.Ir.SaidaP2, comando.P1Valor, BarramentoA);
        //            }
        //        }
        //        else
        //        {
        //            if (comando.P2Info == Palavras.Param.DiretoRegistrador)
        //            {
        //                // Transfere o valor de P2 para o MBR
        //                TransfereDadosPorConectoresNoBarramento(comando.P2Valor, 4, BarramentoA);
        //            }
        //            else if (comando.P2Info == Palavras.Param.IndiretoNumero)
        //            {
        //                //Transfere de P2
        //                p2.atualizaConteudoParametro(p2.getConteudo().Substring(2));
        //                TransfereDadosPorConectoresNoBarramento(Portas.Ir.SaidaP2, Portas.Mbr.Entrada, BarramentoA);
        //            }
        //            else if (comando.P2Info == Palavras.Param.DiretoNumero)
        //            {
        //                //Carrega o próximo valor para P2
        //                p2.atualizaConteudoParametro(null);
        //                CicloDeBusca();
        //                //Transfere de P2
        //                TransfereDadosPorConectoresNoBarramento(Portas.Ir.SaidaP2, Portas.Mbr.Entrada, BarramentoA);
        //            }

        //            // Transfere o valor de P1 para o MAR
        //            p1.atualizaConteudoParametro(p1.getConteudo().Substring(2));
        //            TransfereDadosPorConectoresNoBarramento(Portas.Ir.SaidaP1, Portas.Mar.Entrada, BarramentoA);
        //            //altera modo da memória
        //            Memoria.setEstadoEscrita();
        //            ExecutaMemoria();
        //            Memoria.setEstadoLeitura();
        //        }
        //    }
        //    else if(comando.Opcode == Palavras.Opcode.Add){

        //    }
        //    _iccOld = 0;
        //}

        //public void CicloDeBusca()
        //{
        //    _logger.AddLog("Clico de busca");

        //    _logger.AddLog("T1:");
        //    // transfere dados do PC para o MAR
        //    TransfereDadosPorConectoresNoBarramento(Portas.PC.Saida, Portas.Mar.Entrada, BarramentoA);

        //    _logger.AddLog("T2:");
        //    // transfere dados do MAR para Memoria e em seguida da Memoria para MBR
        //    ExecutaMemoria();

        //    // Incrimenta PC
        //    _logger.AddLog("PC <- PC + 1");
        //    Registradores.PC.AddUnidade();
        //    _logger.AddLog("T3:");
        //    //Movendo do MBR para o IR (p1, p2, opcode)
            
        //    TransfereDadosPorConectoresNoBarramento(Portas.Mbr.Saida, Portas.Ir.EntradaP2, BarramentoA);
        //    TransfereDadosPorConectoresNoBarramento(Portas.Mbr.Saida, Portas.Ir.EntradaP1, BarramentoA);
        //    TransfereDadosPorConectoresNoBarramento(Portas.Mbr.Saida, Portas.Ir.Entrada, BarramentoA);

        //    if (EnderecamentoIndireto(Registradores.IR))
        //    _iccOld = 1;
        //    else
        //        _iccOld = 2;

        //}

        //private void ExecutaMemoria()
        //{
        //    Memoria.ExecutaMemoria();
        //}


        //private bool EnderecamentoIndireto(IComponente Ir)
        //{
        //    var comando = Palavras.LeitorPalavra.Ler(Ir.getConteudo());

        //    return (
        //            comando.P2Info == Palavras.Param.IndiretoNumero ||
        //            comando.P1Info == Palavras.Param.IndiretoRegistrador ||
        //            comando.P1Info == Palavras.Param.IndiretoNumero ||
        //            comando.P2Info == Palavras.Param.IndiretoRegistrador);
        //}

        //public void ClicoIndireto()
        //{
        //    _logger.AddLog("Ciclo de Indireto");
        //    var opcode = Registradores.IR.Opcode;
        //    var p1 = Registradores.IR.P1;
        //    var p2 = Registradores.IR.P2;
        //    int t = 1;
        //    if (p1.getConteudo().Substring(0,2) == Palavras.Param.IndiretoRegistrador)
        //    {
        //        _logger.AddLog("T" + t + ":");
        //        //Remove caracteres de informacao do p1
        //        //o conteudo do P1, nesse caso, é o número do conector do Registrador em questão
        //        var conectorRegistrador = p1.getConteudo().Substring(2);
        //        p1.atualizaConteudoParametro(null);
        //        //Transfere valor do Registrador para P1
        //        TransfereDadosPorConectoresNoBarramento(conectorRegistrador, 16, BarramentoA);
        //        //Muda Informacao para Direta (Como se fosse [NUMERO]) pois a Execucao sabe resolver isso
        //        p1.atualizaConteudoParametro(Palavras.Param.IndiretoNumero + p1.getConteudo().Substring(2));
                
        //    }
        //    else if (p1.getConteudo().Substring(0, 2) == Palavras.Param.IndiretoNumero)
        //    {
        //        p1.atualizaConteudoParametro(null);
        //        //caso for muma referencia [NUMERO] deve buscar o numero na proxima palavra               
        //        CicloDeBusca();
        //    }

        //    if (p2.getConteudo().Substring(0, 2) == Palavras.Param.IndiretoRegistrador)
        //    {
        //        //Remove caracteres de informacao do p2
        //        p2.atualizaConteudoParametro(p2.getConteudo().Substring(2));
        //        //Transfere valor do Registrador para MAR
        //        TransfereDadosPorConectoresNoBarramento(p2.getConteudo(), 3, BarramentoA);
        //        p2.atualizaConteudoParametro(null);
        //        //Busca endereco na memoria
        //        ExecutaMemoria();
        //        //Transfere do MBR para P2
        //        TransfereDadosPorConectoresNoBarramento(Portas.Mbr.Saida, Portas.Ir.EntradaP2, BarramentoA);
        //        p2.atualizaConteudoParametro(Palavras.Param.IndiretoNumero + p2.getConteudo().Substring(2));
        //    }
        //    else if (p2.getConteudo().Substring(0, 2) == Palavras.Param.IndiretoNumero)
        //    {
        //        p2.atualizaConteudoParametro(null);
        //        //caso for um referencia [NUMERO] deve buscar o numero na proxima palavra               
        //        CicloDeBusca(); ;
        //        //Transfere valor do P2 para MAR
        //        TransfereDadosPorConectoresNoBarramento(Portas.Ir.SaidaP2, Portas.Mar.Entrada, BarramentoA);
        //        //Busca endereco na memoria
        //        ExecutaMemoria();
        //        //Transfere do MBR para P2
        //        p2.atualizaConteudoParametro(null);
        //        TransfereDadosPorConectoresNoBarramento(Portas.Mbr.Saida, Portas.Ir.EntradaP2, BarramentoA);
        //        p2.atualizaConteudoParametro(Palavras.Param.IndiretoNumero + p2.getConteudo().Substring(2));
        //    }

        //    _iccOld = 2;
        //}

        //private void TransfereDadosPorConectoresNoBarramento(int conectorLeitura, int conectorEscrita, Barramento barramento)
        //{
        //    // Abre conector para transferir dado
        //    barramento.AbreConector(conectorLeitura);
        //    // Abre conector do para receber o dado
        //    barramento.AbreConector(conectorEscrita);
        //    // Fecha os conectores que foram abertos
        //    barramento.FechaConector(conectorLeitura);
        //    barramento.FechaConector(conectorEscrita);
        //}
        //private void TransfereDadosPorConectoresNoBarramento(string conectorLeituraStr, string conectorEscritaStr, Barramento barramento)
        //{
        //    int conectorLeitura = CalculadoraBinario.BinarioParaInt(conectorLeituraStr);
        //    int conectorEscrita = CalculadoraBinario.BinarioParaInt(conectorEscritaStr);
        //    TransfereDadosPorConectoresNoBarramento(conectorLeitura, conectorEscrita, barramento);
        //}
        //private void TransfereDadosPorConectoresNoBarramento(string conectorLeituraStr, int conectorEscrita, Barramento barramento)
        //{
        //    int conectorLeitura = CalculadoraBinario.BinarioParaInt(conectorLeituraStr);
        //    TransfereDadosPorConectoresNoBarramento(conectorLeitura, conectorEscrita, barramento);
        //}
        //private void TransfereDadosPorConectoresNoBarramento(int conectorLeitura, string conectorEscritaStr, Barramento barramento)
        //{
        //    int conectorEscrita = CalculadoraBinario.BinarioParaInt(conectorEscritaStr);
        //    TransfereDadosPorConectoresNoBarramento(conectorLeitura, conectorEscrita, barramento);
        //}
    }
}
