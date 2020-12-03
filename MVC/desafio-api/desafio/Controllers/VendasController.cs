using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using desafio.Data;
using desafio.DTO;
using desafio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendasController : ControllerBase
    {
        private readonly DataContext Database;
        private readonly IMapper Mapper;
        public VendasController(DataContext database, IMapper mapper)
        {
            this.Mapper = mapper;
            Database = database;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            try{
                var vendas = Database.Vendas
                .Include(v => v.Fornecedor)
                .Include(v => v.Cliente)
                .Include(v => v.ProdutosVenda).ThenInclude(pv => pv.Produto)
                .ToList();

                return Ok(Mapper.Map<IEnumerable<VendaDTO>>(vendas));
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
                var vendas = Database.Vendas
                .Include(v => v.Fornecedor)
                .Include(v => v.Cliente)
                .Include(v => v.ProdutosVenda).ThenInclude(pv => pv.Produto)
                .OrderBy(v => v.DataVenda)
                .ToList();

                return Ok(Mapper.Map<IEnumerable<VendaDTO>>(vendas));
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
                var venda = Database.Vendas
                .Include(v => v.Fornecedor)
                .Include(v => v.Cliente)
                .Include(v => v.ProdutosVenda).ThenInclude(pv => pv.Produto)
                .First(v => v.Id.Equals(id));

                return Ok(Mapper.Map<VendaDTO>(venda));
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return new ObjectResult(new { msg = "Nenhuma Venda encontrada!", erro = e.Message });
            }
        }



        [HttpPost]
        public IActionResult Post([FromBody] VendaDTO vendaBody)
        {
            try {
                Cliente cliente = new Cliente();
                cliente = Database.Clientes.First(c => c.Id == vendaBody.ClienteId);
                if (cliente == null)
                    return BadRequest($"Cliente com Id {vendaBody.ClienteId} não encontrado!");

                Fornecedor fornecedor = new Fornecedor();
                fornecedor = Database.Fornecedores.First(f => f.Id == vendaBody.FornecedorId);
                if (fornecedor == null)
                    return BadRequest($"Cliente com Id {vendaBody.FornecedorId} não encontrado!");

                Venda venda = new Venda();
                venda.Cliente = cliente;
                venda.Fornecedor = fornecedor;
                venda.DataVenda = DateTime.Now;

                double subtotal = 0.0;

                foreach (var pv in vendaBody.ProdutosVenda)
                {
                    ProdutoVenda produtoVenda = new ProdutoVenda();
                    produtoVenda.Quantidade = pv.Quantidade;
                    produtoVenda.Produto = Database.Produtos.First(p => p.Id == pv.ProdutoId);
                    produtoVenda.Venda = venda;

                    if (produtoVenda.Produto.Promocao)
                        subtotal += produtoVenda.Produto.ValorPromocao * produtoVenda.Quantidade;
                    else
                        subtotal += produtoVenda.Produto.Valor * produtoVenda.Quantidade;

                    Database.ProdutosVendas.Add(produtoVenda);
                }
                venda.Total = subtotal;

                Database.Vendas.Add(venda);
                Database.SaveChanges();

                Response.StatusCode = 201;
                return new ObjectResult(new { msg = "Nota Fiscal Salva com Sucesso!", venda = Mapper.Map<VendaDTO>(venda) });

            } catch(Exception err) {
                Response.StatusCode = 401;
                return new ObjectResult(new { msg = "Falha ao Salvar os Dados!", err.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] VendaDTO vendaBody)
        {
            if(id > 0){
                try{
                    Venda venda = Database.Vendas.First(v => v.Id == id);
                    if (venda == null) return BadRequest($"Venda com id {id} não encontrado!");

                    Cliente cliente = new Cliente();
                    cliente = Database.Clientes.First(c => c.Id == vendaBody.ClienteId);
                    if (cliente == null)
                        return BadRequest($"Cliente com Id {vendaBody.ClienteId} não encontrado!");

                    Fornecedor fornecedor = new Fornecedor();
                    fornecedor = Database.Fornecedores.First(f => f.Id == vendaBody.FornecedorId);
                    if (fornecedor == null)
                        return BadRequest($"Cliente com Id {vendaBody.FornecedorId} não encontrado!");

                    venda.Cliente = cliente;
                    venda.Fornecedor = fornecedor;

                    double subtotal = 0.0;

                    var removerRelacao = Database.ProdutosVendas.Where(pv => pv.VendaId == venda.Id).ToList();
                    Database.RemoveRange(removerRelacao);
                    Database.SaveChanges();
                    
                    foreach (var pv in vendaBody.ProdutosVenda)
                    {
                        ProdutoVenda produtoVenda = new ProdutoVenda();
                        produtoVenda.Quantidade = pv.Quantidade;

                        Produto produto = Database.Produtos.First(p => p.Id == pv.ProdutoId);
                        if (fornecedor == null)
                            return BadRequest($"Cliente com Id {vendaBody.FornecedorId} não encontrado!");
                        
                        produtoVenda.Produto = produto;
                        produtoVenda.Venda = venda;

                        if (produtoVenda.Produto.Promocao)
                            subtotal += produtoVenda.Produto.ValorPromocao * produtoVenda.Quantidade;
                        else
                            subtotal += produtoVenda.Produto.Valor * produtoVenda.Quantidade;

                        Database.ProdutosVendas.Add(produtoVenda);
                    }
                    venda.Total = subtotal;
                    
                    Database.SaveChanges();
                    Response.StatusCode = 401;
                    return new ObjectResult(new { msg = "Dados atualizados com Sucesso!", venda = Mapper.Map<VendaDTO>(venda)});
                } catch(Exception e) {
                    Response.StatusCode = 401;
                    return new ObjectResult(new { msg = "Falha ao Atualizar os Dados!", e.Message });
                }
            }
            Response.StatusCode = 404;
            return new ObjectResult(new { msg = $"Id {id} Inválido!" });
        }



        // [HttpPut("{id}")]
        // public IActionResult Put(int id, [FromBody] VendaDTO vendaBody)
        // {
        //     if(id > 0){
        //         try{
        //             Venda venda = Database.Vendas.First(v => v.Id == id);
        //             if (venda == null) return BadRequest($"Venda com id {id} não encontrado!");

        //             Cliente cliente = new Cliente();
        //             cliente = Database.Clientes.First(c => c.Id == vendaBody.ClienteId);
        //             if (cliente == null)
        //                 return BadRequest($"Cliente com Id {vendaBody.ClienteId} não encontrado!");

        //             Fornecedor fornecedor = new Fornecedor();
        //             fornecedor = Database.Fornecedores.First(f => f.Id == vendaBody.FornecedorId);
        //             if (fornecedor == null)
        //                 return BadRequest($"Cliente com Id {vendaBody.FornecedorId} não encontrado!");

        //             venda.Cliente = cliente;
        //             venda.Fornecedor = fornecedor;

        //             double subtotal = 0.0;

        //             var removerRelacao = Database.ProdutosVendas.Where(pv => pv.VendaId == venda.Id).ToList();
        //             Database.RemoveRange(removerRelacao);
        //             Database.SaveChanges();
                    
        //             foreach (var pv in vendaBody.ProdutosVenda)
        //             {
        //                 ProdutoVenda produtoVenda = new ProdutoVenda();
        //                 produtoVenda.Quantidade = pv.Quantidade;

        //                 Produto produto = Database.Produtos.First(p => p.Id == pv.ProdutoId);
        //                 if (fornecedor == null)
        //                     return BadRequest($"Cliente com Id {vendaBody.FornecedorId} não encontrado!");
                        
        //                 produtoVenda.Produto = produto;
        //                 produtoVenda.Venda = venda;

        //                 if (produtoVenda.Produto.Promocao)
        //                     subtotal += produtoVenda.Produto.ValorPromocao * produtoVenda.Quantidade;
        //                 else
        //                     subtotal += produtoVenda.Produto.Valor * produtoVenda.Quantidade;

        //                 Database.ProdutosVendas.Add(produtoVenda);
        //             }
        //             venda.Total = subtotal;
                    
        //             Database.SaveChanges();
        //             Response.StatusCode = 401;
        //             return new ObjectResult(new { msg = "Dados atualizados com Sucesso!", venda = Mapper.Map<VendaDTO>(venda)});
        //         } catch(Exception e) {
        //             Response.StatusCode = 401;
        //             return new ObjectResult(new { msg = "Falha ao Atualizar os Dados!", e.Message });
        //         }
        //     }
        //     Response.StatusCode = 404;
        //     return new ObjectResult(new { msg = $"Id {id} Inválido!" });
        // }
    }
}