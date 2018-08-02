using Componentes.Secundarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Componentes.Principais
{
    public class IR : IComponente
    {
        public Registrador Opcode { get; set; } = new Registrador();
        public Registrador P1 { get; set; } = new Registrador("", "IR_P1");
        public Registrador P2 { get; set; } = new Registrador("", "IR_P2");

        public IR()
        {
        }

        public void setConteudo(string conteudo)
        {
             Opcode.setConteudo(conteudo);
        }

        public string getConteudo()
        {
            return (Opcode.getConteudo() ?? "").PadLeft(16, '0') + 
                    (P1.getConteudo() ?? "").PadLeft(16,'0') + 
                    (P2.getConteudo() ?? "").PadLeft(16, '0');
        }

        public string getName()
        {
            return "IR";
        }

    }

}
