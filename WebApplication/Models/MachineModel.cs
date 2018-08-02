using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class MachineModel
    {
        public int linhaAtual;

        public Guid key { get; set; }
        public List<string> codigo { get; set; } = new List<string>();
        public List<string> log { get; set; } = new List<string>();
        public Registradores registradores { get; set; } = new Registradores();
        public Flags flags { get; set; } = new Flags();
        public List<Dado> memoria { get; set; } = new List<Dado>();
    }

    public class Dado
    {
        public int endereco { get; set; }
        public string valor { get; set; }
        public string enderecoHex { get; set; }
        public string valorHex { get; set; }
    }

    public class Registradores
    {
        public string ax { get; set; }
        public string bx { get; set; }
        public string cx { get; set; }
        public string dx { get; set; }
        public string mar { get; set; }
        public string mbr { get; set; }
        public string pc { get; set; }
    }

    public class Flags
    {
        public bool zeroFlag { get; set; }
        public bool overflowFlag { get; set; }
        public bool signFlag { get; set; }
    }
}
