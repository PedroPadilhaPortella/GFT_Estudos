using System.Linq;
using FuncionariosWA.Data;
using Microsoft.AspNetCore.Mvc;

namespace FuncionariosWA.Controllers
{
    public class VagaController: Controller
    {
        private readonly ApplicationDbContext Database;

        public VagaController(ApplicationDbContext database){
            Database = database;
        }

        public IActionResult Novo()
        {
            return View();
        }

        public IActionResult Salvar()
        {
            return RedirectToAction("Vagas", "Wa");
        }
        
        public IActionResult Editar()
        {
            return View();
        }

        public IActionResult Atualizar()
        {
            return RedirectToAction("Vagas", "Wa");
        }

        public IActionResult Excluir()
        {
            return RedirectToAction("Vagas", "Wa");
        }
    }
}