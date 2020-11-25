using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortellaMarket.Data;
using PortellaMarket.DTO;
using PortellaMarket.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PortellaMarket.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext Database;

        public ProdutosController(ApplicationDbContext database){
            Database = database;
        }
        
        [HttpPost]
        public IActionResult Salvar(ProdutoDTO produtoTemporario){
            if(ModelState.IsValid){
                Produto produto = new Produto();
                produto.Nome = produtoTemporario.Nome;
                produto.Categoria = Database.Categorias.First(cat => cat.Id == produtoTemporario.CategoriaId);
                produto.Fornecedor = Database.Fornecedores.First(forn => forn.Id == produtoTemporario.FornecedorId);
                produto.PrecoDeCusto = float.Parse(produtoTemporario.PrecoDeCustoString, CultureInfo.InvariantCulture.NumberFormat);
                produto.PrecoDeVenda = float.Parse(produtoTemporario.PrecoDeVendaString, CultureInfo.InvariantCulture.NumberFormat);
                produto.Medicao = produtoTemporario.Medicao;
                produto.Status = true;

                Database.Produtos.Add(produto);
                Database.SaveChanges();
                return RedirectToAction("Produtos", "Gestao");
        
            }else{
                ViewBag.Categorias = Database.Categorias.ToList();
                ViewBag.Fornecedores = Database.Fornecedores.ToList();
                return View("../Gestao/NovoProduto");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(ProdutoDTO produtoTemporario){
            if(ModelState.IsValid){
                var produto = Database.Produtos.First(p => p.Id == produtoTemporario.Id);
                produto.Categoria = Database.Categorias.First(cat => cat.Id == produtoTemporario.CategoriaId);
                produto.Fornecedor = Database.Fornecedores.First(forn => forn.Id == produtoTemporario.FornecedorId);
                produto.PrecoDeCusto = float.Parse(produtoTemporario.PrecoDeCustoString, CultureInfo.InvariantCulture.NumberFormat);
                produto.PrecoDeVenda = float.Parse(produtoTemporario.PrecoDeVendaString, CultureInfo.InvariantCulture.NumberFormat);
                produto.Medicao = produtoTemporario.Medicao;

                Database.SaveChanges();
                return RedirectToAction("Produtos", "Gestao");
            }else{
                return View("../Gestao/EditarProduto");
            }
        }

        [HttpPost]
        public IActionResult Excluir(int id){
            if(id > 0){
                var produto = Database.Produtos.First(p => p.Id == id);
                produto.Status = false;
                Database.SaveChanges();
            }
            return RedirectToAction("Produtos", "Gestao");
        }

        [HttpPost]
        public IActionResult Produto(int id) {
            if(id > 0){
                var produto = Database.Produtos.Where(p => p.Status == true).Include(p => p.Categoria).Include(p => p.Fornecedor).First(p => p.Id == id);
                
                if(produto != null){
                    var estoque = Database.Estoques.First(e => e.Produto.Id == produto.Id);
                    if(estoque == null){
                        produto = null;
                    }
                }
                
                if(produto != null){
                    Promocao promocao;
                    try {
                        promocao = Database.Promocoes.First(p => p.Produto.Id == produto.Id && p.Status == true);
                    } catch (Exception) {
                        promocao = null;
                    }

                    if(promocao != null){
                        produto.PrecoDeVenda -= (produto.PrecoDeVenda * (promocao.Porcentagem / 100));
                    }

                    Response.StatusCode = 200;
                    return Json(produto);
                }else{
                    Response.StatusCode = 404;
                    return Json(null);
                }
            }else{
                Response.StatusCode = 404;
                return Json(null);
            }
        }

        [HttpPost]
        public IActionResult Vendas([FromBody] VendaDTO dados) {
            if(dados != null){
                //Gerar venda
                Venda venda = new Venda();
                venda.Total = dados.total;
                venda.Troco = dados.troco;
                venda.ValorPago = dados.troco <= 0.01f ? dados.total : dados.total + dados.troco;
                venda.Data = DateTime.Now;
                Database.Vendas.Add(venda);
                Database.SaveChanges();

                //Salvar saidas
                List<Saida> saidas = new List<Saida>(); 
                foreach (var saidaTemp in dados.produtos) {
                    Saida s = new Saida();
                    s.Quantidade = saidaTemp.quantidade;
                    s.ValorDaVenda = saidaTemp.subtotal;
                    s.Venda = venda;
                    s.Produto = Database.Produtos.First(p => p.Id == saidaTemp.produto);
                    s.Data = DateTime.Now;
                    saidas.Add(s);
                }
                Database.AddRange(saidas);
                Database.SaveChanges();

                System.Console.WriteLine("Dados:");
                System.Console.WriteLine(dados);
                return Ok(dados);
            }else{
                return NotFound("Nenhuma venda registrada!");
            }
        }
    }
}