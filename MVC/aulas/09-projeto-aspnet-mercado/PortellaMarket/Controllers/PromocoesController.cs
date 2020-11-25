using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PortellaMarket.Data;
using PortellaMarket.DTO;
using PortellaMarket.Models;

namespace PortellaMarket.Controllers
{
    public class PromocoesController : Controller
    {
        private readonly ApplicationDbContext Database;

        public PromocoesController(ApplicationDbContext database){
            Database = database;
        }

        [HttpPost]
        public IActionResult Salvar(PromocaoDTO promocaoTemporaria){
            if(ModelState.IsValid){
                Promocao promocao = new Promocao();
                promocao.Id = promocaoTemporaria.Id;
                promocao.Nome = promocaoTemporaria.Nome;
                promocao.Produto = Database.Produtos.First(p => p.Id == promocaoTemporaria.ProdutoId);
                promocao.Porcentagem = promocaoTemporaria.Porcentagem;
                promocao.Status = true;

                Database.Add(promocao);
                Database.SaveChanges();
                return RedirectToAction("Promocoes", "Gestao");
            }else{
                ViewBag.Produtos = Database.Produtos.ToList();
                return View("../Gestao/NovaPromocao");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(PromocaoDTO promocaoTemporaria){
            if(ModelState.IsValid){
                var promocao = Database.Promocoes.First(p => p.Id == promocaoTemporaria.Id);
                promocao.Nome = promocaoTemporaria.Nome;
                promocao.Produto = Database.Produtos.First(p => p.Id == promocaoTemporaria.ProdutoId);
                promocao.Porcentagem = promocaoTemporaria.Porcentagem;

                Database.SaveChanges();
                return RedirectToAction("Promocoes", "Gestao");
            }else{
                return View("../Gestao/EditarPromocao");
            }
        }

        public IActionResult Excluir(int id){
            if(id > 0){
                var promocao = Database.Promocoes.First(p => p.Id == id);
                promocao.Status = false;
                Database.SaveChanges();
            }
            return RedirectToAction("Promocoes", "Gestao");
        }
    }
}