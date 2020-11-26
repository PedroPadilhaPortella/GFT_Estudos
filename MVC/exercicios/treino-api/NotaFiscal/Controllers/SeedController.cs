using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotaFiscal.Data;
using NotaFiscal.Models;

namespace NotaFiscal.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Authorize(Roles = "Admin")]
    public class SeedController : ControllerBase
    {
        private readonly NotaFiscalContext Database;
        public SeedController(NotaFiscalContext database)
        {
            this.Database = database;
        }

        [HttpPost]
        public IActionResult SeedData()
        {
            if (!(Database.Produtos.Any()))
            {
                Database.AddRange(
                    new Produto(1, "Arroz 5kg", 24.30, true),
                    new Produto(2, "Feijão 1kg", 9.7, true),
                    new Produto(3, "Cx de Ovos 12 unidades", 12.78, true),
                    new Produto(4, "Sabão em Pó Omo", 3.5, true),
                    new Produto(5, "Doritos 400g", 14.0, true),
                    new Produto(6, "Macarrão Adria", 1.5, true),
                    new Produto(7, "Sorvete Limão 2l", 11.65, true),
                    new Produto(8, "AK 47 Pente Alongado", 9800.00, true),
                    new Produto(9, "Cx Ammo AK-47", 52.00, true)
                );
            }

            if (!(Database.Clientes.Any()))
            {
                Database.AddRange(
                    new Cliente(1, "Pedro Henrique Padilha Portella", "54239856874", "04345230", "11978439842", true),
                    new Cliente(2, "Samuel Moraes", "45627490211", "98576740", "13976540988", true),
                    new Cliente(3, "William Matrix", "54396393755", "97546350", "11965848873", true),
                    new Cliente(4, "Rodrigo Silva" ,"46579865463", "86754976", "11987868431", true),
                    new Cliente(5, "Binho", "7349562295832", "78432643", "11976768321", true),
                    new Cliente(6, "Rafael Souza", "43758342916", "94754233", "11933409090", true),
                    new Cliente(7, "Barbara", "54353237809", "93345483", "11988976543",true)
                );
            }

            Database.SaveChanges();

            if(!(Database.NotasFiscais.Any()))
            {
                Database.AddRange(
                    new NotaFiscal.Models.NotaFiscal(1, new DateTime(2020, 09, 12), 53.4, Database.Clientes.First(c => c.Id == 2)),
                    new NotaFiscal.Models.NotaFiscal(2, new DateTime(2020, 10, 25), 28.0, Database.Clientes.First(c => c.Id == 7)),
                    new NotaFiscal.Models.NotaFiscal(3, new DateTime(2020, 07, 03), 24.28, Database.Clientes.First(c => c.Id == 3)),
                    new NotaFiscal.Models.NotaFiscal(4, new DateTime(2020, 12, 30), 1060.0, Database.Clientes.First(c => c.Id == 1))
                );
            }

            Database.SaveChanges();
            
            if(!(Database.ProdutosNotaFiscal.Any()))
            {
                Database.AddRange(
                    new ProdutoNotaFiscal(1, Database.NotasFiscais.First(n => n.Id.Equals(1)), Database.Produtos.First(p => p.Id.Equals(1))),
                    new ProdutoNotaFiscal(3, Database.NotasFiscais.First(n => n.Id.Equals(1)), Database.Produtos.First(p => p.Id.Equals(2))),
                    new ProdutoNotaFiscal(2, Database.NotasFiscais.First(n => n.Id.Equals(2)), Database.Produtos.First(p => p.Id.Equals(5))),
                    new ProdutoNotaFiscal(1, Database.NotasFiscais.First(n => n.Id.Equals(3)), Database.Produtos.First(p => p.Id.Equals(3))),
                    new ProdutoNotaFiscal(2, Database.NotasFiscais.First(n => n.Id.Equals(3)), Database.Produtos.First(p => p.Id.Equals(4))),
                    new ProdutoNotaFiscal(3, Database.NotasFiscais.First(n => n.Id.Equals(3)), Database.Produtos.First(p => p.Id.Equals(6))),
                    new ProdutoNotaFiscal(1, Database.NotasFiscais.First(n => n.Id.Equals(4)), Database.Produtos.First(p => p.Id.Equals(8))),
                    new ProdutoNotaFiscal(5, Database.NotasFiscais.First(n => n.Id.Equals(4)), Database.Produtos.First(p => p.Id.Equals(9)))
                );
            }

            Database.SaveChanges();

            return Ok("Dados Semeados com Sucesso!");
        }
    }
}