//using Componentes.Principais;
using Componentes.Principais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class MachineData
    {
        public Guid Key { get; set; }
        public List<string> codigo { get; set; } = new List<string>();
        public List<string> log { get; set; } = new List<string>();
        public UC uc { get; set; }
    }
}
