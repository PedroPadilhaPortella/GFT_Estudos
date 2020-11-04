using System.Linq;
using FuncionariosWA.Data;
using FuncionariosWA.Models;
using Microsoft.AspNetCore.Mvc;

namespace FuncionariosWA.Controllers
{
    public class GFTController : Controller
    {
        private readonly ApplicationDbContext Database;

        public GFTController(ApplicationDbContext database){
            Database = database;
        }

        public IActionResult NovaGFT()
        {
            return View();
        }
        public IActionResult SalvarGFT(GFT localDeTrabalho)
        {
            if(ModelState.IsValid){
                GFT gft = new GFT();
                gft.Nome = localDeTrabalho.Nome;
                gft.Cep = localDeTrabalho.Nome;
                gft.Endereco = localDeTrabalho.Endereco;
                gft.Cidade = localDeTrabalho.Cidade;
                gft.Estado = localDeTrabalho.Estado;
                gft.Status = true;

                Database.GFT.Add(gft);
                Database.SaveChanges();
                return RedirectToAction("LocaisDeTrabalho", "Wa");
            }else{
                return View("../GFT/NovaGFT");
            }
        }

        public IActionResult EditarGFT(int id)
        {
            var gft = Database.GFT.First(gft => gft.Id == id);
            return View(gft);
        }
        public IActionResult AtualizarGFT(GFT local)
        {
            if(ModelState.IsValid){
                GFT gft = Database.GFT.First(gft => gft.Id == local.Id);
                gft.Nome = local.Nome;
                gft.Cep = local.Cep;
                gft.Endereco = local.Endereco;
                gft.Cidade = local.Cidade;
                gft.Estado = local.Estado;

                Database.SaveChanges();
                return RedirectToAction("LocaisDeTrabalho", "Wa");
            }else{
                return View("../GFT/EditarGFT");
            }
        }
        public IActionResult ExcluirGFT(int id)
        {
            if(id > 0){
                var gft = Database.GFT.First(gft => gft.Id == id);
                gft.Status = false;
                Database.SaveChanges();
            }
            return RedirectToAction("LocaisDeTrabalho", "Wa");
        }
    }
}