using System.Linq;
using FuncionariosWA.Data;
using FuncionariosWA.Models;
using FuncionariosWA.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FuncionariosWA.Controllers
{
    public class CargoController : Controller
    {
        private readonly ApplicationDbContext Database;

        public CargoController(ApplicationDbContext database)
        {
            Database = database;
        }

        public IActionResult Novo()
        {
            return View();
        }

        public IActionResult Salvar(CargoDTO cargoT)
        {
            if (ModelState.IsValid)
            {
                Cargo cargo = new Cargo();
                cargo.Nome = cargoT.Nome;
                cargo.Status = true;

                Database.Cargos.Add(cargo);
                Database.SaveChanges();
                return RedirectToAction("Cargos", "Wa");
            }
            else
            {
                return View("../Cargo/Novo");
            }
        }
        public IActionResult Editar(int id)
        {
            var cargo = Database.Cargos.First(c => c.Id == id);
            CargoDTO cargoView = new CargoDTO();
            cargoView.Id = cargo.Id;
            cargoView.Nome = cargo.Nome;
            return View(cargo);
        }
        public IActionResult Atualizar(CargoDTO cargoT)
        {
            if (ModelState.IsValid)
            {
                Cargo cargo = Database.Cargos.First(t => t.Id == cargoT.Id);
                cargo.Nome = cargoT.Nome;
                Database.SaveChanges();
                return RedirectToAction("Cargos", "Wa");
            }
            else
            {
                return View("../Cargo/Editar");
            }
        }
        public IActionResult Excluir(int id)
        {
            if (id > 0)
            {
                var cargo = Database.Cargos.First(c => c.Id == id);
                cargo.Status = false;
                Database.SaveChanges();
            }
            return RedirectToAction("Cargos", "Wa");
        }
    }
}