using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using desafio.Data;
using desafio.DTO;
using desafio.HATEOAS;
using desafio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VendasController : ControllerBase
    {
        private readonly DataContext Database;
        private readonly IMapper Mapper;
        private Hateoas HATEOAS;
        public VendasController(DataContext database, IMapper mapper)
        {
            this.Mapper = mapper;
            Database = database;
            HATEOAS = new Hateoas("localhost:5001/api/vendas");
            HATEOAS.AddAction("get_venda", "GET");
            HATEOAS.AddAction("edit_venda", "PATCH");
            HATEOAS.AddAction("update_venda", "PUT");
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            try{
                var vendasDB = Database.Vendas
                .Include(v => v.Fornecedor)
                .Include(v => v.Cliente)
                .Include(v => v.ProdutosVenda).ThenInclude(pv => pv.Produto)
                .ToList();

                var vendas = Mapper.Map<IEnumerable<VendaDTO>>(vendasDB);
                List<VendaHATEOAS> vendasHATEOAS = new List<VendaHATEOAS>();
                foreach (var venda in vendas)
                {
                    VendaHATEOAS vendaHATEOAS = new VendaHATEOAS();
                    vendaHATEOAS.venda = venda;
                    vendaHATEOAS.links = HATEOAS.GetActions(venda.Id.ToString());
                    vendasHATEOAS.Add(vendaHATEOAS);
                }
                return Ok(vendasHATEOAS);
            }
            catch (Exception e) {
                Response.StatusCode = 500;
                return new ObjectResult(new { msg = "Nenhuma Venda encontrada!", erro = e.Message });
            }
        }


        [HttpGet("data")]
        public IActionResult GetAllOrderByDate()
        {
            try{
                var vendasDB = Database.Vendas
                .Include(v => v.Fornecedor)
                .Include(v => v.Cliente)
                .Include(v => v.ProdutosVenda).ThenInclude(pv => pv.Produto)
                .OrderBy(v => v.DataVenda)
                .ToList();

                var vendas = Mapper.Map<IEnumerable<VendaDTO>>(vendasDB);
                List<VendaHATEOAS> vendasHATEOAS = new List<VendaHATEOAS>();
                foreach (var venda in vendas)
                {
                    VendaHATEOAS vendaHATEOAS = new VendaHATEOAS();
                    vendaHATEOAS.venda = venda;
                    vendaHATEOAS.links = HATEOAS.GetActions(venda.Id.ToString());
                    vendasHATEOAS.Add(vendaHATEOAS);
                }
                return Ok(vendasHATEOAS);
            }
            catch (Exception e) {
                Response.StatusCode = 500;
                return new ObjectResult(new { msg = "Nenhuma Venda encontrada!", erro = e.Message });
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var vendaDB = Database.Vendas
                .Include(v => v.Fornecedor)
                .Include(v => v.Cliente)
                .Include(v => v.ProdutosVenda).ThenInclude(pv => pv.Produto)
                .FirstOrDefault(v => v.Id.Equals(id));

                var venda = Mapper.Map<VendaDTO>(vendaDB);
                VendaHATEOAS vendaHATEOAS = new VendaHATEOAS();
                vendaHATEOAS.venda = venda;
                vendaHATEOAS.links = HATEOAS.GetActions(venda.Id.ToString());
                return Ok(vendaHATEOAS);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return new ObjectResult(new { msg = "Nenhuma Venda encontrada!", erro = e.Message });
            }
        }




        /// <summary>
        /// Método responsável por registrar uma Venda, insira apenas o clienteId, fornecedorId e um array de ProdutosVenda com cada produtoId e sua quantidade,
        /// os outros campos serão gerados automaticamente.
        ///Exemplo:  { "clienteId": 1, "fornecedorId": 7, "produtosVenda": [ { "quantidade": 5, "produtoId": 3 }, { "quantidade": 7, "produtoId": 6 } ] }
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] VendaDTO vendaBody)
        {
            try {
                Cliente cliente = new Cliente();
                try {
                    cliente = Database.Clientes.First(c => c.Id == vendaBody.ClienteId);
                } catch(Exception e) {
                    Response.StatusCode = 404;
                    return new ObjectResult(new { msg = $"Cliente com Id {vendaBody.ClienteId} não encontrado, forneca um ClienteId válido", e.Message });
                }

                Fornecedor fornecedor = new Fornecedor();
                try {
                    fornecedor = Database.Fornecedores.First(f => f.Id == vendaBody.FornecedorId);
                } catch(Exception e) {
                    Response.StatusCode = 404;
                    return new ObjectResult(new { msg = $"Fornecedor com Id {vendaBody.FornecedorId} não encontrado, forneca um FornecedorId válido!", e.Message });
                }

                Venda venda = new Venda();
                venda.Cliente = cliente;
                venda.Fornecedor = fornecedor;
                venda.DataVenda = DateTime.Now;

                double subtotal = 0.0;

                if(vendaBody.ProdutosVenda == null)
                    return BadRequest("Não podemos cadastrar uma Venda sem Itens: [ 'ProdutosVenda': {Produto: N e Quantidade: N } ]");

                foreach (var pv in vendaBody.ProdutosVenda)
                {
                    if(pv.Quantidade <= 0)
                        return BadRequest("A Quantidade dos Produtos precisa ser maior que zero!");

                    Produto produto = new Produto();
                    try {
                        produto = Database.Produtos.FirstOrDefault(p => p.Id == pv.ProdutoId);
                    } catch(Exception e) {
                        Response.StatusCode = 404;
                        return new ObjectResult(new { msg = $"Produto com Id {pv.ProdutoId} não encontrado, forneca um ProdutoId válido!", e.Message });
                    }

                    ProdutoVenda produtoVenda = new ProdutoVenda();
                    produtoVenda.Quantidade = pv.Quantidade;
                    produtoVenda.Produto = produto;
                    produtoVenda.Venda = venda;

                    if (produtoVenda.Produto.Promocao == true)
                        subtotal += produtoVenda.Produto.ValorPromocao * produtoVenda.Quantidade;
                    else
                        subtotal += produtoVenda.Produto.Valor * produtoVenda.Quantidade;

                    Database.ProdutosVendas.Add(produtoVenda);
                }

                venda.Total = subtotal;

                Database.Vendas.Add(venda);
                Database.SaveChanges();

                Response.StatusCode = 201;
                return new ObjectResult(new { msg = "Venda Salva com Sucesso!", venda = Mapper.Map<VendaDTO>(venda) });

            } catch(Exception err) {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Falha ao Salvar os Dados!", err.Message });
            }
        }



        /// <summary>
        /// Método responsável por atualizar completamente um Fornecedor, insira apenas o clienteId, fornecedorId e um array de ProdutosVenda com cada produtoId e sua quantidade,
        /// os outros campos não podem ser atualizados.
        ///Exemplo:  { "clienteId": 1, "fornecedorId": 7, "produtosVenda": [ { "quantidade": 5, "produtoId": 3 }, { "quantidade": 7, "produtoId": 6 } ] }
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] VendaDTO vendaBody)
        {
            try {
                Cliente cliente = new Cliente();
                try {
                    cliente = Database.Clientes.FirstOrDefault(c => c.Id == vendaBody.ClienteId);
                } catch(Exception e) {
                    Response.StatusCode = 404;
                    return new ObjectResult(new { msg = $"Cliente com Id {vendaBody.ClienteId} não encontrado, forneca um ClienteId válido!", e.Message });
                }

                Fornecedor fornecedor = new Fornecedor();
                try {
                    fornecedor = Database.Fornecedores.FirstOrDefault(f => f.Id == vendaBody.FornecedorId);
                } catch(Exception e) {
                    Response.StatusCode = 404;
                    return new ObjectResult(new { msg = $"Fornecedor com Id {vendaBody.FornecedorId} não encontrado, forneca um FornecedorId válido!", e.Message });
                }

                Venda venda = Database.Vendas.FirstOrDefault(v => v.Id.Equals(id));
                venda.Cliente = cliente;
                venda.Fornecedor = fornecedor;

                double subtotal = 0.0;

                var produtosVendasRemover = Database.ProdutosVendas.Where(n => n.VendaId == venda.Id).ToList();
                Database.ProdutosVendas.RemoveRange(produtosVendasRemover);
                Database.SaveChanges();

                foreach (var pv in vendaBody.ProdutosVenda)
                {
                    if(pv.Quantidade <= 0)
                        return BadRequest("A Quantidade dos Produtos precisa ser maior que zero!");

                    Produto produto = new Produto();
                    try {
                        produto = Database.Produtos.FirstOrDefault(p => p.Id == pv.ProdutoId);
                    } catch(Exception e) {
                        Response.StatusCode = 404;
                        return new ObjectResult(new { msg = $"Produto com Id {pv.ProdutoId} não encontrado, forneca um ProdutoId válido!", e.Message });
                    }

                    ProdutoVenda produtoVenda = new ProdutoVenda();
                    produtoVenda.Quantidade = pv.Quantidade;
                    produtoVenda.Produto = produto;
                    produtoVenda.Venda = venda;

                    if (produtoVenda.Produto.Promocao == true)
                        subtotal += produtoVenda.Produto.ValorPromocao * produtoVenda.Quantidade;
                    else
                        subtotal += produtoVenda.Produto.Valor * produtoVenda.Quantidade;

                    Database.ProdutosVendas.Add(produtoVenda);
                }

                venda.Total = subtotal;

                Database.SaveChanges();

                Response.StatusCode = 201;
                return new ObjectResult(new { msg = "Venda Atualizada com Sucesso!", venda = Mapper.Map<VendaDTO>(venda) });

            } catch(Exception err) {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Falha ao Atualizar os Dados!", err.Message });
            }
        }



        /// <summary>
        /// Método responsável por atualizar parcialmente um Fornecedor, insira opcionalmente o clienteId, fornecedorId e um array de ProdutosVenda com cada produtoId e sua quantidade,
        /// os outros campos não podem ser atualizados.
        ///Exemplo:  { "clienteId": 1, "fornecedorId": 7, "produtosVenda": [ { "quantidade": 5, "produtoId": 3 }, { "quantidade": 7, "produtoId": 6 } ] }
        /// </summary>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] VendaDTO vendaBody)
        {
            try {
                Venda venda = Database.Vendas.Include(v => v.Cliente).Include(v => v.Fornecedor).Include(v => v.ProdutosVenda).FirstOrDefault(v => v.Id.Equals(id));

                Cliente cliente = Database.Clientes.FirstOrDefault(c => c.Id == vendaBody.ClienteId);
                venda.Cliente = cliente != null ? cliente : venda.Cliente;

                Fornecedor fornecedor = Database.Fornecedores.FirstOrDefault(f => f.Id == vendaBody.FornecedorId);
                venda.Fornecedor = fornecedor != null ? fornecedor : venda.Fornecedor;

                double subtotal = 0.0;

                if(vendaBody.ProdutosVenda != null)
                {
                    var produtosVendasRemover = Database.ProdutosVendas.Where(n => n.VendaId == venda.Id).ToList();
                    Database.ProdutosVendas.RemoveRange(produtosVendasRemover);
                    Database.SaveChanges();

                    foreach (var pv in vendaBody.ProdutosVenda)
                    {
                        if(pv.Quantidade <= 0)
                            return BadRequest("A Quantidade dos Produtos precisa ser maior que zero!");

                        Produto produto = new Produto();
                        try {
                            produto = Database.Produtos.FirstOrDefault(p => p.Id == pv.ProdutoId);
                        } catch(Exception e) {
                            Response.StatusCode = 404;
                            return new ObjectResult(new { msg = $"Produto com Id {pv.ProdutoId} não encontrado, forneca um ProdutoId válido!", e.Message });
                        }

                        ProdutoVenda produtoVenda = new ProdutoVenda();
                        produtoVenda.Quantidade = pv.Quantidade;
                        produtoVenda.Produto = produto;
                        produtoVenda.Venda = venda;

                        if (produtoVenda.Produto.Promocao == true)
                            subtotal += produtoVenda.Produto.ValorPromocao * produtoVenda.Quantidade;
                        else
                            subtotal += produtoVenda.Produto.Valor * produtoVenda.Quantidade;

                        Database.ProdutosVendas.Add(produtoVenda);
                    }
                    
                    venda.Total = subtotal;
                }

                Database.SaveChanges();

                Response.StatusCode = 201;
                return new ObjectResult(new { msg = "Venda Atualizada com Sucesso!", venda = Mapper.Map<VendaDTO>(venda) });

            } catch(Exception err) {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Falha ao Atualizar os Dados!", err.Message });
            }
        }
    }
}