using System.Linq;
using FuncionariosWA.Data;
using FuncionariosWA.Models;
using FuncionariosWA.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FuncionariosWA.Controllers
{
    [Authorize]
    public class LocalDeTrabalhoController : Controller
    {
        private readonly ApplicationDbContext Database;

        public LocalDeTrabalhoController(ApplicationDbContext database)
        {
            Database = database;
        }

        public IActionResult Novo()
        {
            return View();
        }
        public IActionResult Salvar(LocalDeTrabalhoDTO localT)
        {
            if (ModelState.IsValid)
            {
                LocalDeTrabalho localDeTrabalho = new LocalDeTrabalho();
                localDeTrabalho.Nome = localT.Nome;
                localDeTrabalho.Cep = localT.Nome;
                localDeTrabalho.Endereco = localT.Endereco;
                localDeTrabalho.Cidade = localT.Cidade;
                localDeTrabalho.Estado = localT.Estado;
                localDeTrabalho.Telefone = localT.Telefone;
                localDeTrabalho.Status = true;

                Database.LocaisDeTrabalho.Add(localDeTrabalho);
                Database.SaveChanges();
                return RedirectToAction("LocaisDeTrabalho", "Wa");
            }
            else
            {
                return View("../LocalDeTrabalho/Novo");
            }
        }

        public IActionResult Editar(int id)
        {
            var localDeTrabalho = Database.LocaisDeTrabalho.First(gft => gft.Id == id);
            LocalDeTrabalhoDTO localDeTrabalhoView = new LocalDeTrabalhoDTO();
            localDeTrabalhoView.Id = localDeTrabalho.Id;
            localDeTrabalhoView.Nome = localDeTrabalho.Nome;
            localDeTrabalhoView.Cep = localDeTrabalho.Cep;
            localDeTrabalhoView.Endereco = localDeTrabalho.Endereco;
            localDeTrabalhoView.Cidade = localDeTrabalho.Cidade;
            localDeTrabalhoView.Estado = localDeTrabalho.Estado;
            localDeTrabalhoView.Telefone = localDeTrabalho.Telefone;

            return View(localDeTrabalhoView);
        }
        public IActionResult Atualizar(LocalDeTrabalhoDTO localT)
        {
            if (ModelState.IsValid)
            {
                LocalDeTrabalho localDeTrabalho = Database.LocaisDeTrabalho.First(gft => gft.Id == localT.Id);
                localDeTrabalho.Nome = localT.Nome;
                localDeTrabalho.Cep = localT.Cep;
                localDeTrabalho.Endereco = localT.Endereco;
                localDeTrabalho.Cidade = localT.Cidade;
                localDeTrabalho.Estado = localT.Estado;
                localDeTrabalho.Telefone = localT.Telefone;

                Database.SaveChanges();
                return RedirectToAction("LocaisDeTrabalho", "Wa");
            }
            else
            {
                return View("../LocalDeTrabalho/Editar");
            }
        }
        public IActionResult Excluir(int id)
        {
            if (id > 0)
            {
                var gft = Database.LocaisDeTrabalho.First(gft => gft.Id == id);
                gft.Status = false;
                Database.SaveChanges();
            }
            return RedirectToAction("LocaisDeTrabalho", "Wa");
        }
    }
}