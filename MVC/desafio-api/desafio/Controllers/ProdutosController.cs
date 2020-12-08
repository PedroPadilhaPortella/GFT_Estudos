using System;
using System.Linq;
using desafio.Data;
using desafio.Models;
using desafio.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using desafio.HATEOAS;

namespace desafio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProdutosController : ControllerBase
    {
        private readonly DataContext Database;
        private readonly IMapper Mapper;
        private Hateoas HATEOAS;
        public ProdutosController(DataContext database, IMapper mapper)
        {
            this.Mapper = mapper;
            Database = database;
            HATEOAS = new Hateoas("localhost:5001/api/produtos");
            HATEOAS.AddAction("get_produto", "GET");
            HATEOAS.AddAction("edit_produto", "PATCH");
            HATEOAS.AddAction("update_produto", "PUT");
            HATEOAS.AddAction("delete_produto", "DELETE");
        }



        [HttpGet]
        public IActionResult Get() {
            try {
                var produtosDB = Database.Produtos.Where(c => c.Status == true).Include(p => p.Fornecedor).ToList();
                var produtos = Mapper.Map<IEnumerable<ProdutoDTO>>(produtosDB);
                List<ProdutoHATEOAS> produtosHATEOAS = new List<ProdutoHATEOAS>();
                foreach (var produto in produtos)
                {
                    ProdutoHATEOAS produtoHATEOAS = new ProdutoHATEOAS();
                    produtoHATEOAS.produto = produto;
                    produtoHATEOAS.links = HATEOAS.GetActions(produto.Id.ToString());
                    produtosHATEOAS.Add(produtoHATEOAS);
                }
                return Ok(produtosHATEOAS);
            } catch (Exception e) {
                Response.StatusCode = 500;
                return new ObjectResult(new { msg = "Nenhum Produto encontrado!", erro = e.Message });
            }
        }

        [HttpGet("asc")]
        public IActionResult GetOrderByNameAscending() {
            try {
                var produtosDB = Database.Produtos.Where(p => p.Status == true).Include(p => p.Fornecedor).OrderBy(p => p.Nome).ToList();
                var produtos = Mapper.Map<IEnumerable<ProdutoDTO>>(produtosDB);
                List<ProdutoHATEOAS> produtosHATEOAS = new List<ProdutoHATEOAS>();
                foreach (var produto in produtos)
                {
                    ProdutoHATEOAS produtoHATEOAS = new ProdutoHATEOAS();
                    produtoHATEOAS.produto = produto;
                    produtoHATEOAS.links = HATEOAS.GetActions(produto.Id.ToString());
                    produtosHATEOAS.Add(produtoHATEOAS);
                }
                return Ok(produtosHATEOAS);
            } catch (Exception e) {
                Response.StatusCode = 500;
                return new ObjectResult(new { msg = "Nenhum Produto encontrado!", erro = e.Message });
            }
        }

        [HttpGet("desc")]
        public IActionResult GetOrderByNameDescending() {
            try {
                var produtosDB = Database.Produtos.Where(p => p.Status == true).Include(p => p.Fornecedor).OrderByDescending(p => p.Nome).ToList();
                var produtos = Mapper.Map<IEnumerable<ProdutoDTO>>(produtosDB);
                List<ProdutoHATEOAS> produtosHATEOAS = new List<ProdutoHATEOAS>();
                foreach (var produto in produtos)
                {
                    ProdutoHATEOAS produtoHATEOAS = new ProdutoHATEOAS();
                    produtoHATEOAS.produto = produto;
                    produtoHATEOAS.links = HATEOAS.GetActions(produto.Id.ToString());
                    produtosHATEOAS.Add(produtoHATEOAS);
                }
                return Ok(produtosHATEOAS);
            } catch (Exception e) {
                Response.StatusCode = 500;
                return new ObjectResult(new { msg = "Nenhum Produto encontrado!", erro = e.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            try {
                var produtoDB = Database.Produtos.Where(c => c.Status == true).Include(p => p.Fornecedor).First(c => c.Id == id);
                var produto = Mapper.Map<ProdutoDTO>(produtoDB);
                ProdutoHATEOAS produtoHATEOAS = new ProdutoHATEOAS();
                produtoHATEOAS.produto = produto;
                produtoHATEOAS.links = HATEOAS.GetActions(produto.Id.ToString());
                return Ok(produtoHATEOAS);
            } catch (Exception e) {
                Response.StatusCode = 500;
                return new ObjectResult(new { msg = $"Produto com Id {id} não encontrado!", erro = e.Message });
            }
        }


        [HttpGet("nome/{nome}")]
        public IActionResult GetByName(string nome) {
            try {
                var produtoDB = Database.Produtos.Where(c => c.Status == true).Include(p => p.Fornecedor).First(c => c.Nome.Contains(nome));
                var produto = Mapper.Map<ProdutoDTO>(produtoDB);
                ProdutoHATEOAS produtoHATEOAS = new ProdutoHATEOAS();
                produtoHATEOAS.produto = produto;
                produtoHATEOAS.links = HATEOAS.GetActions(produto.Id.ToString());
                return Ok(produtoHATEOAS);
            } catch (Exception e) {
                Response.StatusCode = 500;
                return new ObjectResult(new { msg = $"Produto com nome {nome} não encontrado!", erro = e.Message });
            }
        }




        /// <summary>
        /// Método responsável por cadastrar um Produto, insira apenas o nome, codigo, valor, promocao, valorPromocao, quantidade, fornecedorId e categoria, os outros campos serão gerados automaticamente.
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] ProdutoDTO produto) {
            try {
                if (produto.Nome.Length <= 1 || String.IsNullOrEmpty(produto.Nome) || String.IsNullOrWhiteSpace(produto.Nome))
                    return new BadRequestObjectResult(new { msg = "Nome do Produto Nulo ou Inválido" });

                if (produto.Codigo == null || String.IsNullOrEmpty(produto.Codigo) || String.IsNullOrWhiteSpace(produto.Codigo))
                    return new BadRequestObjectResult(new { msg = "Código do Produto Nulo ou Inválido!" });

                if (produto.Valor == 0.0)
                    return new BadRequestObjectResult(new { msg = "Valor do Produto Inválido!" });

                if (produto.ValorPromocao == 0.0)
                    return new BadRequestObjectResult(new { msg = "Valor Promocional do Produto Inválido!" });

                if (produto.Quantidade < 0)
                    return new BadRequestObjectResult(new { msg = " Quantidade do Produto Inválido!" });

                if (produto.FornecedorId < 0)
                    return new BadRequestObjectResult(new { msg = " Id do Fornecedor Nulo ou Inválido" });

                if (produto.Categoria == null || String.IsNullOrEmpty(produto.Categoria) || String.IsNullOrWhiteSpace(produto.Categoria))
                    return new BadRequestObjectResult(new { msg = "Categoria do Produto Inválida!" });

                Produto produtoDB = new Produto();
                produtoDB.Nome = produto.Nome;
                produtoDB.Codigo = produto.Codigo;
                produtoDB.Valor = produto.Valor;
                produtoDB.Promocao = produto.Promocao;
                produtoDB.ValorPromocao = produto.ValorPromocao;
                produtoDB.Categoria = produto.Categoria;
                produtoDB.Imagem = $"{produto.Nome}.jpg".Replace(" ", "-");
                produtoDB.Quantidade = produto.Quantidade;
                produtoDB.Status = true;
                produtoDB.Fornecedor = Database.Fornecedores.First(f => f.Id.Equals(produto.FornecedorId));

                Database.Add(produtoDB);
                Database.SaveChanges();
                Response.StatusCode = 201;
                return new ObjectResult(new { msg = "Produto Cadastrado com Sucesso!", produtoDB });
            } catch (Exception e)
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Erro ao cadastrar Produto!", e.Message });
            }
        }




        /// <summary>
        /// Método responsável por atualizar completamente um Fornecedor, insira apenas o nome, codigo, valor, promocao, valorPromocao, quantidade, fornecedorId e categoria, os outros campos não podem ser atualizados.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProdutoDTO produtoBody)
        {
            try {
                if (produtoBody.Nome.Length <= 1 || String.IsNullOrEmpty(produtoBody.Nome) || String.IsNullOrWhiteSpace(produtoBody.Nome))
                    return new BadRequestObjectResult(new { msg = "Nome do Produto Nulo ou Inválido" });

                if (produtoBody.Codigo == null || String.IsNullOrEmpty(produtoBody.Codigo) || String.IsNullOrWhiteSpace(produtoBody.Codigo))
                    return new BadRequestObjectResult(new { msg = "Código do Produto Nulo ou Inválido!" });

                if (produtoBody.Valor == 0.0)
                    return new BadRequestObjectResult(new { msg = "Valor do Produto Inválido!" });

                if (produtoBody.ValorPromocao == 0.0)
                    return new BadRequestObjectResult(new { msg = "Valor Promocional do Produto Inválido!" });

                if (produtoBody.Quantidade < 0)
                    return new BadRequestObjectResult(new { msg = " Quantidade do Produto Inválido!" });

                if (produtoBody.Categoria == null || String.IsNullOrEmpty(produtoBody.Categoria) || String.IsNullOrWhiteSpace(produtoBody.Categoria))
                    return new BadRequestObjectResult(new { msg = "Categoria do Produto Inválida!" });

                Produto produto = Database.Produtos.Where(p => p.Status == true).First(p => p.Id == id);
                if (produto == null) return BadRequest("Produto não encontrado!");

                produto.Nome = produtoBody.Nome;
                produto.Codigo = produtoBody.Codigo;
                produto.Valor = produtoBody.Valor;
                produto.Promocao = produtoBody.Promocao;
                produto.ValorPromocao = produtoBody.ValorPromocao;
                produto.Imagem = $"{produto.Nome}.jpg".Replace(" ", "-");
                produto.Quantidade = produtoBody.Quantidade;
                produto.Fornecedor = Database.Fornecedores.First(f => f.Id == produtoBody.FornecedorId);
                produto.Categoria = produtoBody.Categoria;

                Database.SaveChanges();
                Response.StatusCode = 201;
                return new ObjectResult(new { msg = "Produto Atualizado com Sucesso!", produto });
            } catch (Exception e)
            {
                Response.StatusCode = 401;
                return new ObjectResult(new { msg = "Falha ao Atualizar Produto!", erro = e.Message });
            }
        }



        /// <summary>
        /// Método responsável por atualizar parcialmente um Produto, insira opcionalmente o nome, codigo, valor, promocao, valorPromocao, quantidade, fornecedorId e categoria, os outros campos não podem ser atualizados.
        /// </summary>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] ProdutoDTO produtoBody) {
            if (id > 0)
            {
                try {
                    var produto = Database.Produtos.Include(p => p.Fornecedor).First(f => f.Id == id);
                    if (produto == null) return BadRequest("produto não encontrado!");

                    if (produto != null)
                    {
                        produto.Nome = produtoBody.Nome != null ? produtoBody.Nome : produto.Nome;
                        produto.Codigo = produtoBody.Codigo != null ? produtoBody.Codigo : produto.Codigo;
                        produto.Valor = produtoBody.Valor != 0.0 ? produtoBody.Valor : produto.Valor;
                        produto.Promocao = produtoBody.Promocao ? produtoBody.Promocao : produto.Promocao;
                        produto.ValorPromocao = produtoBody.ValorPromocao != 0.0 ? produtoBody.ValorPromocao : produto.ValorPromocao;
                        produto.Categoria = produtoBody.Categoria != null ? produtoBody.Categoria : produto.Categoria;
                        produto.Imagem = produtoBody.Imagem != null ? $"{produto.Nome}.jpg".Replace(" ", "-") : produto.Imagem;
                        produto.Quantidade = produtoBody.Quantidade != 0 ? produtoBody.Quantidade : produto.Quantidade;
                        produto.Fornecedor = produtoBody.FornecedorId != 0 ? Database.Fornecedores.First(f => f.Id == produtoBody.FornecedorId) : produto.Fornecedor;

                        Database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new { msg = "Produto atualizado com Sucesso!", produto });
                    }
                    else
                    {
                        Response.StatusCode = 404;
                        return new ObjectResult(new { msg = "Produto não encontrado!" });
                    }
                } catch (Exception e) {
                    Response.StatusCode = 404;
                    return new ObjectResult(new { msg = $"Erro ao Atualizar Produto com id {id}, verifique os dados!", e.Message });
                }
            } else {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Id do Produto Inválido" });
            }
        }



        /// <summary>
        /// Método responsável por remover um Produto, mas não se preocupe, ele não será apagado do banco, apenas desativado, pois ele pode estar sendo usado nos registros de vendas.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            if (id > 0) {
                try {
                    var produto = Database.Produtos.Where(p => p.Status == true).First(p => p.Id.Equals(id));
                    if (produto == null) return BadRequest("Produto não encontrado!");

                    produto.Status = false;
                    Database.SaveChanges();
                    Response.StatusCode = 200;
                    return new ObjectResult(new { msg = "Produto removido com Sucesso!" });
                } catch (Exception e) {
                    Response.StatusCode = 401;
                    return new ObjectResult(new { msg = "Falha ao Remover Produto!", erro = e.Message });
                }
            } else {
                Response.StatusCode = 401;
                return new ObjectResult(new { msg = "Falha ao Remover Produto, Id Inválido !" });
            }
        }
    }
}