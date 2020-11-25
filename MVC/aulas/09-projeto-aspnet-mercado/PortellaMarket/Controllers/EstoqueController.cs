using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PortellaMarket.Data;
using PortellaMarket.Models;

namespace PortellaMarket.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly ApplicationDbContext Database;

        public EstoqueController(ApplicationDbContext database){
            Database = database;
        }

        [HttpPost]
        public IActionResult Salvar(Estoque estoqueTemporario){
            Database.Estoques.Add(estoqueTemporario);
            Database.SaveChanges();
            return RedirectToAction("Estoque", "Gestao");
        }

        [HttpPost]
        public IActionResult Atualizar(Estoque estoqueTemporario){
            var estoque = Database.Estoques.First(e => e.Id == estoqueTemporario.Id);
            estoque.Produto = Database.Produtos.First(p => p.Id == estoqueTemporario.ProdutoId);
            estoque.Quantidade = estoqueTemporario.Quantidade;

            Database.SaveChanges();
            return RedirectToAction("Estoque", "Gestao");
        }

        public IActionResult Excluir(int id){
            if(id > 0){
                var estoque = Database.Estoques.First(e => e.Id == id);
                Database.Remove(estoque);
                Database.SaveChanges();
            }
            return RedirectToAction("Estoque", "Gestao");
        }
    }
}