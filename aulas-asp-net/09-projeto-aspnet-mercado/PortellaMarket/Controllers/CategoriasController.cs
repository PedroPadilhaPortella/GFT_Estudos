using Microsoft.AspNetCore.Mvc;
using PortellaMarket.Data;
using PortellaMarket.DTO;
using PortellaMarket.Models;
using System;
using System.Linq;

namespace PortellaMarket.Controllers
{
    public class CategoriasController : Controller
    {

        private readonly ApplicationDbContext Database;

        public CategoriasController(ApplicationDbContext database){
            Database = database;
        }

        [HttpPost]
        public IActionResult Salvar(CategoriaDTO categoriaTemporaria) {
            if(ModelState.IsValid){
                Categoria categoria = new Categoria();
                categoria.Nome = categoriaTemporaria.Nome;
                categoria.Status = true;
                Database.Categorias.Add(categoria);
                Database.SaveChanges();
                return RedirectToAction("Categorias", "Gestao");
            }else{
                return View("../Gestao/NovaCategoria");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(CategoriaDTO categoriaTemporaria) {
            if(ModelState.IsValid){
                Categoria categoria = Database.Categorias.First(cat => cat.Id == categoriaTemporaria.Id);
                categoria.Nome = categoriaTemporaria.Nome;
                Database.SaveChanges();
                return RedirectToAction("Categorias", "Gestao");
            }else{
                return View("../Gestao/EditarCategoria");
            }
        }

        [HttpPost]
        public IActionResult Excluir(int id) {
            if(id > 0){
                var categoria = Database.Categorias.First(c => c.Id == id);
                categoria.Status = false;
                Database.SaveChanges();
            }
            return RedirectToAction("Categorias", "Gestao");
        }
    }
}