using System;
using System.Collections.Generic;
using System.Text;

namespace Componentes.Secundarios
{
    public interface IComponente
    {
        void setConteudo(string conteudo);
        string getConteudo();
        string getName();
    }
}
