using System.Linq;
using FuncionariosWA.Data;
using FuncionariosWA.Models;
using Microsoft.AspNetCore.Mvc;

namespace FuncionariosWA.Controllers
{
    public class TecnologiaController : Controller
    {
        private readonly ApplicationDbContext Database;

        public TecnologiaController(ApplicationDbContext database){
            Database = database;
        }

        public IActionResult Novo()
        {
            return View();
        }
        public IActionResult Salvar(Tecnologia tecnologiaT)
        {
            if(ModelState.IsValid){
                Tecnologia tecnologia = new Tecnologia();
                tecnologia.Nome = tecnologiaT.Nome;
                tecnologia.Status = true;
                Database.Tecnologias.Add(tecnologia);
                Database.SaveChanges();
                return RedirectToAction("Tecnologias", "Wa");
            }else{
                return View("../Tecnologia/Novo");
            }
        }
        public IActionResult Editar(int id)
        {
            var tecnologia = Database.Tecnologias.First(t => t.Id == id);
            return View(tecnologia);
        }
        public IActionResult Atualizar(Tecnologia tecnologiaT)
        {
            if(ModelState.IsValid){
                Tecnologia tecnologia = Database.Tecnologias.First(t => t.Id == tecnologiaT.Id);
                tecnologia.Nome = tecnologiaT.Nome;
                Database.SaveChanges();
                return RedirectToAction("Tecnologias", "Wa");
            }else{
                return View("../Tecnologia/Editar");
            }
        }
        public IActionResult Excluir(int id)
        {
            if(id > 0){
                var categoria = Database.Tecnologias.First(t => t.Id == id);
                categoria.Status = false;
                Database.SaveChanges();
            }
            return RedirectToAction("Tecnologias", "Wa");
        }
    }
}