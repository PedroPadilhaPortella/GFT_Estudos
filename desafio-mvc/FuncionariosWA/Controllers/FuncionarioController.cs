using System.Linq;
using FuncionariosWA.Data;
using Microsoft.AspNetCore.Mvc;

namespace FuncionariosWA.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly ApplicationDbContext Database;

        public FuncionarioController(ApplicationDbContext database){
            Database = database;
        }

        public IActionResult NovoFuncionario()
        {
            ViewBag.LocalDeTrabalho = Database.GFT.ToList();

            return View();
        }

        public IActionResult SalvarFuncionario()
        {
            //TO DO a data de termino é 15 dias dps do inicio
            return RedirectToAction("Wa", "Funcionarios");
        }
    }
}