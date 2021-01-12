using System;
using System.Linq;
using api.Data;
using api.Models;
using api.HATEOAS;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace api.Controllers
{
    [Route("v1/[controller]")] //versionamento de api, legada, sem suporte!
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        /* SWAGGER*/
        /*  Padrão/Biblioteca de Documentação de API's REST, mapeando retornos, status code, endpoints e etc, e ainda gera umA página html
        com a documentação da API e gera rotas de teste da sua API.*/
        private readonly ApplicationDbContext Database;
        private HATEOAS.HATEOAS HATEOAS;

        public ProdutosController(ApplicationDbContext database)
        {
            this.Database = database;

            //Criação do objeto HATEOS
            HATEOAS = new HATEOAS.HATEOAS("localhost:5001/v1/Produtos");
            HATEOAS.AddAction("get_product", "GET");
            HATEOAS.AddAction("delete_product", "DELETE");
            HATEOAS.AddAction("edit_product", "PATCH");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var produtos = Database.Produtos.ToList();
            List<ProdutoContainer> produtosHATEOAS = new List<ProdutoContainer>();

            foreach(var produto in produtos){
                ProdutoContainer produtoHATEOS = new ProdutoContainer();
                produtoHATEOS.produto = produto;
                produtoHATEOS.links = HATEOAS.GetActions(produto.Id.ToString());
                produtosHATEOAS.Add(produtoHATEOS);
            }

            return Ok(produtosHATEOAS.ToArray()); //200
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try {
                Produto produto = Database.Produtos.First(p => p.Id == id);

                ProdutoContainer produtoHATEOAS = new ProdutoContainer();
                produtoHATEOAS.produto = produto;
                produtoHATEOAS.links = HATEOAS.GetActions(produto.Id.ToString());

                return Ok(produtoHATEOAS);
            } catch (Exception) {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Produto não encontrado!" });
                // return BadRequest(new {msg = "Produto não encontrado!"}); //400 
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProdutoTemporario produtoT)
        {
            /* Validação de dados */

            if(produtoT.Preco <= 0) {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "O Preço do Produto não pode ser menor que 0.0"});
            }

            if(produtoT.Nome.Length <= 1) {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "O Nome do Produto precisa ter mais de 1 caractere"});
            }

            if(String.IsNullOrEmpty(produtoT.Nome) || String.IsNullOrWhiteSpace(produtoT.Nome)) {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Nome do Produto Nulo ou Inválido"});
            }

            Produto produto = new Produto();
            produto.Nome = produtoT.Nome;
            produto.Preco = produtoT.Preco;

            Database.Produtos.Add(produto);
            Database.SaveChanges();

            Response.StatusCode = 201;
            return new ObjectResult(new { msg = "Produto criado com Sucesso!" });
            // return Ok(new {msg = "Produto criado com Sucesso!"}); //200
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try {
                Produto produto = Database.Produtos.First(p => p.Id == id);
                Database.Produtos.Remove(produto);
                Database.SaveChanges();
                return Ok(new {msg = "Produto removido com Sucesso!"});

            } catch (Exception) {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Produto não encontrado!" });
            }
        }

        [HttpPatch]
        public IActionResult Patch([FromBody] Produto produtoBody)
        {
            if(produtoBody.Id > 0) {

                try {
                    var produto = Database.Produtos.First(p => p.Id == produtoBody.Id);

                    if(produto != null) {

                        produto.Nome = produtoBody.Nome != null ? produtoBody.Nome : produto.Nome;
                        produto.Preco = produtoBody.Preco != 0.0 ? produtoBody.Preco : produto.Preco;

                        Database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new { msg = "Produto atualizado com Sucesso!" });
                    }else {
                        Response.StatusCode = 404;
                        return new ObjectResult(new { msg = "Produto não encontrado!" });
                    }
                } catch(Exception) {
                    Response.StatusCode = 404;
                    return new ObjectResult(new { msg = $"Produto com id {produtoBody.Id} não encontrado!" });
                }
            
            }else {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Id do Produto Inválido" });
            }
        }
    }
}