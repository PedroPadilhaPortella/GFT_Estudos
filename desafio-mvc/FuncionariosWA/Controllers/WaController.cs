using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FuncionariosWA.Models;
using FuncionariosWA.Data;

namespace FuncionariosWA.Controllers
{
    public class WaController : Controller
    {
        private readonly ApplicationDbContext Database;

        public WaController(ApplicationDbContext database){
            Database = database;
        }

        [HttpGet("")]
        public IActionResult Wa()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

//Actions de Locais de Trabalho GFT //
        public IActionResult LocaisDeTrabalho()
        {
            var locaisDeTrabalho = Database.GFT.Where(gft => gft.Status == true).ToList();
            return View(locaisDeTrabalho);
        }

//Actions de Tecnologias //
        public IActionResult Tecnologias()
        {
            var tecnologia = Database.Tecnologias.Where(t => t.Status == true).ToList();
            return View(tecnologia);
        }

//Actions de Funcionarios //
        public IActionResult Funcionarios()
        {
            // var funcionario = Database.Funcionarios.Where(f => f.Status == true).ToList();
            return View();
        }
        
//Actions de Erro
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
