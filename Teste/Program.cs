using Componentes;
using Componentes.Helpers;
using Componentes.Principais;
using Componentes.Secundarios;
using System;
using System.Collections.Generic;

namespace Teste
{
    class Program
    {
        static void Main(string[] args)
        {

            var codigo = new List<string>();

            //codigo.Add("mov ax,bx");
            //codigo.Add("mov ax,[100]");
            //codigo.Add("mov ax,[bx]");
            //codigo.Add("mov [100],bx");
            codigo.Add("mov [100],10");
            ////codigo.Add("mov ax,[100]");
            ////codigo.Add("mov [ax],[bx]");
            //codigo.Add("mov [100],bx");
            UC comp = CriaUC(codigo);

            comp.Registradores.BX.setConteudo("100000000");
            //comp.Registradores.AX.setConteudo("00");

            comp.Rodar();
            comp.Rodar();
            comp.Rodar();
            comp.Rodar();
            comp.Rodar();
            comp.Rodar();
            comp.Rodar();
            comp.Rodar();
            comp.Rodar();
            comp.Rodar();
            //comp.Clock();
            //comp.Clock();
            //comp.Clock();
            //comp.Clock();
            //comp.Clock();
            //comp.Clock();
            //comp.Clock();
            //comp.Clock();

        }

        private static UC CriaUC(List<string> codigo)
        {
            var compilado = Compilador.Compilar(codigo);

            UC comp = new UC(new Logger(), compilado.CodigoXBinario);

            foreach (var item in compilado.Codigo)
                comp.Memoria.AddCodigo(item);
            return comp;
        }
    }

    public class Logger : ILog
    {
        public void AddLog(string line)
        {
            Console.WriteLine(line);
        }
    }

}
