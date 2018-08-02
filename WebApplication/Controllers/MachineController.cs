using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Componentes.Helpers;
using Componentes.Principais;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/VirtualMachine")]
    public class MachineController : Controller
    {
        public static List<MachineData> _data { get; set; } = new List<MachineData>();


        [HttpPost()]
        public IActionResult CreateMachine([FromBody] List<string> code)
        {
            var compilado = Compilador.Compilar(code);

            if (compilado.Erros.Count > 0)
                return BadRequest(compilado.Erros);

            var newUc = CriaUC(code, compilado);
            var virtualUc = new MachineData();
            virtualUc.codigo = code;
            virtualUc.Key = Guid.NewGuid();
            virtualUc.uc = newUc;
            _data.Add(virtualUc);
            return Ok(PcToResult(virtualUc));
        }
        [HttpGet("{key}")]
        public IActionResult GetMachine(Guid key)
        {
            var pc = _data.FirstOrDefault(c => key == c.Key);
            if (pc == null)
                return NotFound();

            return Ok(PcToResult(pc));
        }
        [HttpGet("RunStep/{key}")]
        public IActionResult RunStep(Guid key)
        {
            var pc = _data.FirstOrDefault(c => key == c.Key);
            if (pc == null)
                return NotFound();

            pc.uc.RodarInstrucao();

            return Ok(PcToResult(pc));
        }

        private static MachineModel PcToResult(MachineData pc)
        {
            var result = new MachineModel();
            result.key = pc.Key;
            result.codigo = pc.codigo;
            result.log = pc.log;
            result.linhaAtual = pc.uc.LinhaCodigoAtual();
            result.registradores.ax = pc.uc.Registradores.AX.getConteudo().ToHex(4);
            result.registradores.bx = pc.uc.Registradores.BX.getConteudo().ToHex(4);
            result.registradores.cx = pc.uc.Registradores.CX.getConteudo().ToHex(4);
            result.registradores.dx = pc.uc.Registradores.DX.getConteudo().ToHex(4);
            result.registradores.mar = pc.uc.Registradores.MAR.getConteudo().ToHex(4);
            result.registradores.mbr = pc.uc.Registradores.MBR.getConteudo().ToHex(4);
            result.registradores.pc = pc.uc.Registradores.PC.getConteudo().ToHex(4);
            result.memoria = pc.uc.Memoria.getMemoriaToda().Select(c => new Models.Dado
            {
                valor = c.Conteudo,
                valorHex = c.Conteudo.ToHex(4),
                endereco = c.Endereco,
                enderecoHex = IntParaBinario(c.Endereco,16).ToString().ToHex(4)
            }).ToList();
            return result;
        }

        private static UC CriaUC(List<string> codigo, Compilado compilado)
        {

            UC comp = new UC(null, compilado.CodigoXBinario);

            foreach (var item in compilado.Codigo)
                comp.Memoria.AddCodigo(item);
            return comp;
        }

        public static string IntParaBinario(int numero, int tamanho)
        {
            return Convert.ToString(numero, 2).PadLeft(tamanho, '0');
        }

    }
}
