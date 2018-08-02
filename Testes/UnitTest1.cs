using Componentes.Helpers;
using Componentes.Secundarios;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using System;
using System.Collections.Generic;

namespace Testes
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// MOV AX,BX
        /// </summary>
        [TestMethod]
        public void TesteMovAxBx()
        {
            var l = new List<string>();
            var codigo = new List<string>();
            codigo.Add("mov ax,bx");
            l.Add(Palavras.Opcode.Mov + Palavras.Param.DiretoRegistrador + "0110" + Palavras.Param.DiretoRegistrador + "1001");
            
            try
            {
                ValidaCódigo(l, codigo);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// MOV AX,10
        /// </summary>
        [TestMethod]
        public void TesteAxDecimal()
        {
            var l = new List<string>();
            var codigo = new List<string>();
            codigo.Add("mov ax,10");
            l.Add(Palavras.Opcode.Mov + Palavras.Param.DiretoRegistrador + "0110" + Palavras.Param.DiretoNumero + "0000");
            l.Add(Palavras.Opcode.DADO + Palavras.Param.DiretoNumero + "0000001010");

            try
            {
                ValidaCódigo(l, codigo);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

             
        }
        /// <summary>
        /// MOV AX,[100]
        /// </summary>
        [TestMethod]
        public void TesteAx_Decimal_()
        {
            var l = new List<string>();
            var codigo = new List<string>();
            //// MOV AX,[100]
            codigo.Add("mov ax,[100]");
            l.Add(Palavras.Opcode.Mov + Palavras.Param.DiretoRegistrador + "0110" + Palavras.Param.IndiretoNumero + "0000");
            l.Add(Palavras.Opcode.DADO + Palavras.Param.DiretoNumero + "0001100100");
            try
            {
                ValidaCódigo(l, codigo);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
        /// <summary>
        /// MOV AX, [BX]
        /// </summary>
        [TestMethod]
        public void TesteAx_BX_()
        {
            var l = new List<string>();
            var codigo = new List<string>();
            //// MOV AX,[BX]
            codigo.Add("mov ax,[bx]");
            l.Add(Palavras.Opcode.Mov + Palavras.Param.DiretoRegistrador + "0110" + Palavras.Param.IndiretoRegistrador + "1001");

            try
            {
                ValidaCódigo(l, codigo);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            
        }

        public void Z()
        {
            ////// MOV [AX],100
            //codigo.Add("mov [ax],100");
            //l.Add(Palavras.Opcode.Mov + Palavras.Param.IndiretoRegistrador + "0110" + Palavras.Param.DiretoNumero + "0000");
            //l.Add(Palavras.Opcode.DADO + Palavras.Param.DiretoNumero + "0001100100");
            ////// MOV [AX],100
            //codigo.Add("mov [ax],bx");
            //l.Add(Palavras.Opcode.Mov + Palavras.Param.IndiretoRegistrador + "0110" + Palavras.Param.DiretoRegistrador + "1001");
            ////// MOV [100],10
            //codigo.Add("mov [100],10");
            //l.Add(Palavras.Opcode.Mov + Palavras.Param.IndiretoNumero + "0000" + Palavras.Param.DiretoNumero + "0000");
            //l.Add(Palavras.Opcode.DADO + Palavras.Param.DiretoNumero + "0001100100");
            //l.Add(Palavras.Opcode.DADO + Palavras.Param.DiretoNumero + "0000001010");
            ////// MOV [100],bx
            //codigo.Add("mov [100],bx");
            //l.Add(Palavras.Opcode.Mov + Palavras.Param.IndiretoNumero + "0000" + Palavras.Param.DiretoRegistrador + "1001");
            //l.Add(Palavras.Opcode.DADO + Palavras.Param.DiretoNumero + "0001100100");

        }

        private static void ValidaCódigo(List<string> l, List<string> codigo)
        {
            var c= Compilador.Compilar(codigo);
            var compilado = c.Codigo;
            if (compilado.Count != l.Count)
                throw new Exception();

            for (int i = 0; i < compilado.Count; i++)
            {
                if (compilado[i] != l[i])
                    throw new Exception();
            }
        }
    }
}
