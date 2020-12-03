using System;
using System.Linq;
using CryptSharp;
using desafio.Data;
using desafio.Models;
using Microsoft.AspNetCore.Mvc;

namespace NotaFiscal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly DataContext Database;
        public SeedController(DataContext database)
        {
            this.Database = database;
        }

        [HttpPost]
        public IActionResult SeedData()
        {
            try{
                if (!(Database.Clientes.Any()))
                {
                    Database.AddRange(
                            new Cliente(1, "Pedro Portella", "pedro@gmail.com", Crypter.MD5.Crypt("pedro2020"), "00000000-00", "Cliente", true, new DateTime(2020, 12, 02)),
                            new Cliente(2, "Thomas Shelby", "thomas@gmail.com", Crypter.MD5.Crypt("thomas2020"), "04562343-30", "Cliente", true, new DateTime(2020, 12, 02)),
                            new Cliente(3, "Ragnar Lothbrok", "ragnar@gmail.com", Crypter.MD5.Crypt("ragnar2020"), "08823430-90", "Cliente", true, new DateTime(2020, 12, 02)),
                            new Cliente(4, "Frank Castle", "frank@gmail.com", Crypter.MD5.Crypt("frank2020"), "432434234324-00", "Cliente", true, new DateTime(2020, 12, 02)),
                            new Cliente(5, "Conan Doyle", "conan@gmail.com", Crypter.MD5.Crypt("conan2020"), "00234234232-00", "Cliente", true, new DateTime(2020, 12, 02))
                    );
                }

                if (!(Database.Fornecedores.Any()))
                {
                    Database.AddRange(
                        new Fornecedor(1, "AlcaFoods", "23432423443242342", true),
                        new Fornecedor(2, "Pursuit Batata", "432324343242342", true),
                        new Fornecedor(3, "Enhance Fire", "65868567674752712", true),
                        new Fornecedor(4, "Quantum Devs", "51541646164164161", true),
                        new Fornecedor(5, "Irish Grow", "46264265616467576573", true),
                        new Fornecedor(6, "Portella GFT", "514647577242511", true),
                        new Fornecedor(7, "Mercadinho do Luciano", "3253465477373772", true)
                    );
                }

                Database.SaveChanges();

                if (!(Database.Produtos.Any()))
                {
                    Database.AddRange(
                        new Produto(1, "Cereal Matinal Chocolate", "001CEMACHALFO", 12.0, false, 10.0, "Cereais", 1200, Database.Fornecedores.First(f => f.Id == 1)),
                        new Produto(2, "Batata Doce Nacional kg", "002BADONAPUBA", 6.50, false, 5.0, "HortiFrutti", 500, Database.Fornecedores.First(f => f.Id == 2)),
                        new Produto(3, "Cx de Ferramentas EF", "003CXFEEF", 120.0, true, 99.90, "Ferramentas e Equipamentos", 120, Database.Fornecedores.First(f => f.Id == 3)),
                        new Produto(4, "GTA San Andreas 7", "004GTASAQD", 280.0, true, 150.0, "Jogos", 12, Database.Fornecedores.First(f => f.Id == 4)),
                        new Produto(5, "Caderno do Minecraft 90 folhas", "005CAMIIRGR", 12.0, false, 11.99, "Escolares", 800, Database.Fornecedores.First(f => f.Id == 5)),
                        new Produto(6, "Software Portella Market", "006SOPOMAGFT", 190000.0, false, 19000.0, "Softwares", 5, Database.Fornecedores.First(f => f.Id == 6)),
                        new Produto(7, "Sucrilhos Flocos e Banana kg", "007SUFLBALAFO", 9.0, false, 8.0, "Cereais", 1200, Database.Fornecedores.First(f => f.Id == 1)),
                        new Produto(8, "Call of Duty WW2", "008CADUWWQUDE", 300.0, false, 200.0, "Jogos", 30, Database.Fornecedores.First(f => f.Id == 4)),
                        new Produto(9, "Candy Crush Mobile", "009CACRMOQUDE", 2.0, false, 2.0, "Jogos", 100, Database.Fornecedores.First(f => f.Id == 4)),
                        new Produto(10, "Software WebApi .Net Angular", "010SOWEANPOGFT", 7800.0, false, 7800.0, "Softwares", 12, Database.Fornecedores.First(f => f.Id == 6))
                    );
                }

                Database.SaveChanges();

                if (!(Database.Vendas.Any()))
                {
                    Database.AddRange(
                        new Venda(1, Database.Fornecedores.First(f => f.Id == 7), Database.Clientes.First(c => c.Id == 1), 73.0, new DateTime(2020, 10, 10)),
                        new Venda(2, Database.Fornecedores.First(f => f.Id == 7), Database.Clientes.First(c => c.Id == 2), 452.0, new DateTime(2020, 10, 10)),
                        new Venda(3, Database.Fornecedores.First(f => f.Id == 7), Database.Clientes.First(c => c.Id == 3), 63.0, new DateTime(2020, 10, 10)),
                        new Venda(4, Database.Fornecedores.First(f => f.Id == 7), Database.Clientes.First(c => c.Id == 4), 7899.9, new DateTime(2020, 10, 10)),
                        new Venda(5, Database.Fornecedores.First(f => f.Id == 7), Database.Clientes.First(c => c.Id == 5), 674.0, new DateTime(2020, 10, 10))
                    );
                }

                Database.SaveChanges();

                if (!(Database.ProdutosVendas.Any()))
                {
                    Database.AddRange(
                        new ProdutoVenda(5, Database.Vendas.First(v => v.Id == 1), Database.Produtos.First(p => p.Id == 1)),
                        new ProdutoVenda(2, Database.Vendas.First(v => v.Id == 1), Database.Produtos.First(p => p.Id == 2)),

                        new ProdutoVenda(1, Database.Vendas.First(v => v.Id == 2), Database.Produtos.First(p => p.Id == 4)),
                        new ProdutoVenda(1, Database.Vendas.First(v => v.Id == 2), Database.Produtos.First(p => p.Id == 8)),
                        new ProdutoVenda(1, Database.Vendas.First(v => v.Id == 2), Database.Produtos.First(p => p.Id == 9)),

                        new ProdutoVenda(3, Database.Vendas.First(v => v.Id == 3), Database.Produtos.First(p => p.Id == 5)),
                        new ProdutoVenda(3, Database.Vendas.First(v => v.Id == 3), Database.Produtos.First(p => p.Id == 7)),

                        new ProdutoVenda(1, Database.Vendas.First(v => v.Id == 4), Database.Produtos.First(p => p.Id == 3)),
                        new ProdutoVenda(1, Database.Vendas.First(v => v.Id == 4), Database.Produtos.First(p => p.Id == 10)),

                        new ProdutoVenda(3, Database.Vendas.First(v => v.Id == 5), Database.Produtos.First(p => p.Id == 1)),
                        new ProdutoVenda(4, Database.Vendas.First(v => v.Id == 5), Database.Produtos.First(p => p.Id == 2)),
                        new ProdutoVenda(2, Database.Vendas.First(v => v.Id == 5), Database.Produtos.First(p => p.Id == 7)),
                        new ProdutoVenda(6, Database.Vendas.First(v => v.Id == 5), Database.Produtos.First(p => p.Id == 3))
                    );
                }

                Database.SaveChanges();
                
                Response.StatusCode = 201;
                return new ObjectResult(new { msg = "Dados Semeados com Sucesso!" });
            } catch(Exception erro) {
                Response.StatusCode = 500;
                return new ObjectResult(new { msg = "Falha ao Semear os Dados", erro.Message });
            }
        }
    }
}