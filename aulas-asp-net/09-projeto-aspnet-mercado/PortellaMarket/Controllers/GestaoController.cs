using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortellaMarket.Data;
using PortellaMarket.DTO;
using PortellaMarket.Models;

namespace PortellaMarket.Controllers
{
    [Authorize]
    public class GestaoController : Controller
    {
        private readonly ApplicationDbContext Database;

        public GestaoController(ApplicationDbContext database){
            Database = database;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Categorias()
        {
            var categorias = Database.Categorias.Where(c => c.Status == true).ToList();
            return View(categorias);
        }
        
        public IActionResult NovaCategoria()
        {
            return View();
        }

        public IActionResult EditarCategoria(int id)
        {
            var categoria = Database.Categorias.First(c => c.Id == id);

            //A view EditarCategoria só recebe dados do tipo CategoriaDTO, então é preciso criar uma CategoriaDTO para receber os dados
            CategoriaDTO categoriaView = new CategoriaDTO();
            categoriaView.Id = categoria.Id;
            categoriaView.Nome = categoria.Nome;

            return View(categoriaView);
        }

        public IActionResult Fornecedores()
        {
            var fornecedores = Database.Fornecedores.Where(f => f.Status == true).ToList();
            return View(fornecedores);
        }

        public IActionResult NovoFornecedor()
        {
            return View();
        }

        public IActionResult EditarFornecedor(int id)
        {
            var fornecedor = Database.Fornecedores.First(f => f.Id == id);

            FornecedorDTO fornecedorView = new FornecedorDTO();
            fornecedorView.Id = fornecedor.Id;
            fornecedorView.Nome = fornecedor.Nome;
            fornecedorView.Email = fornecedor.Email;
            fornecedorView.Telefone = fornecedor.Telefone;
            
            return View(fornecedorView);
        }

        public IActionResult Produtos()
        {
            var produtos = Database.Produtos.Include(p => p.Categoria).Include(p => p.Fornecedor).Where(p => p.Status == true).ToList();
            return View(produtos);
        }

        public IActionResult NovoProduto()
        {
            ViewBag.Categorias = Database.Categorias.ToList();
            ViewBag.Fornecedores = Database.Fornecedores.ToList();
            return View();
        }

        public IActionResult EditarProduto(int id)
        {
            var produto = Database.Produtos.Include(p => p.Categoria).Include(p => p.Fornecedor).First(p => p.Id == id);
            ProdutoDTO produtoView = new ProdutoDTO();
            produtoView.Id= produto.Id;
            produtoView.Nome = produto.Nome;
            produtoView.CategoriaId = produto.Categoria.Id;
            produtoView.FornecedorId = produto.Fornecedor.Id;
            produtoView.PrecoDeCusto = produto.PrecoDeCusto;
            produtoView.PrecoDeVenda = produto.PrecoDeVenda;
            produtoView.Medicao = produto.Medicao;

            ViewBag.Categorias = Database.Categorias.ToList();
            ViewBag.Fornecedores = Database.Fornecedores.ToList();
            
            return View(produtoView);
        }

        public IActionResult Promocoes()
        {
            var promocoes = Database.Promocoes.Include(p => p.Produto).Where(p => p.Status == true).ToList();
            return View(promocoes);
        }

        public IActionResult NovaPromocao()
        {
            ViewBag.Produtos = Database.Produtos.ToList();
            return View();
        }

        public IActionResult EditarPromocao(int id)
        {
            var promocao = Database.Promocoes.Include(p => p.Produto).First(p => p.Id == id);
            PromocaoDTO promocaoView = new PromocaoDTO();
            promocaoView.Id= promocao.Id;
            promocaoView.Nome = promocao.Nome;
            promocaoView.ProdutoId = promocao.Produto.Id;
            promocaoView.Porcentagem = promocao.Porcentagem;

            ViewBag.Produtos = Database.Produtos.ToList();
            
            return View(promocaoView);
        }

        public IActionResult Estoque()
        {
            var listaEstoques = Database.Estoques.Include(p => p.Produto).ToList();
            return View(listaEstoques);
        }

        public IActionResult NovoEstoque()
        {
            ViewBag.Produtos = Database.Produtos.ToList();
            return View();
        }
        public IActionResult EditarEstoque(int id)
        {
            var estoque = Database.Estoques.Include(e => e.Produto).First(p => p.Id == id);
            Estoque estoqueView = new Estoque();
            estoqueView.Id= estoque.Id;
            estoqueView.ProdutoId = estoque.Produto.Id;
            estoqueView.Quantidade = estoque.Quantidade;

            ViewBag.Produtos = Database.Produtos.ToList();
            
            return View(estoqueView);
        }

        public IActionResult Vendas()
        {
            var relatorioVendas = Database.Vendas.ToList();
            return View(relatorioVendas);
        }

        [HttpPost]
        public IActionResult RelatorioDeVendas()
        {
            return base.Ok(Database.Vendas.ToList());
        }
    }
}