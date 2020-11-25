using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NotaFiscal.Data;
using NotaFiscal.DTO;
using NotaFiscal.Models;

namespace NotaFiscal.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly NotaFiscalContext _database;
        public ProdutosController(NotaFiscalContext database)
        {
            this._database = database;
        }

        [HttpGet]
        public IActionResult GetProdutos(){
            var produtos = _database.Produtos.Where(p => p.Status == true).ToList();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetProdutosById(int id){
            try {
                Produto produto = _database.Produtos.First(p => p.Id == id);
                return Ok(produto);
            } catch (Exception) {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Produto não encontrado!" });
            }
        }

        [HttpPost]
        public IActionResult PostProdutos([FromBody] ProdutoDTO produtoDTO){

            if(produtoDTO.PrecoUnitario <= 0) {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "O Preço do Produto não pode ser menor que 0.0"});
            }

            if(produtoDTO.Nome.Length <= 1) {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "O Nome do Produto precisa ter mais de 1 caractere"});
            }

            if(String.IsNullOrEmpty(produtoDTO.Nome) || String.IsNullOrWhiteSpace(produtoDTO.Nome)) {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Nome do Produto Nulo ou Inválido"});
            }

            Produto produto = new Produto();
            produto.Nome = produtoDTO.Nome;
            produto.PrecoUnitario = produtoDTO.PrecoUnitario;
            produto.Status = true;

            _database.Produtos.Add(produto);
            _database.SaveChanges();

            Response.StatusCode = 201;
            return new ObjectResult(new { msg = "Produto criado com Sucesso!" });
        }

        [HttpPatch]
        public IActionResult PatchProduto([FromBody] Produto produtoBody)
        {
            if(produtoBody.Id > 0) {

                try {
                    var produto = _database.Produtos.First(p => p.Id == produtoBody.Id);

                    if(produto != null) {

                        produto.Nome = produtoBody.Nome != null ? produtoBody.Nome : produto.Nome;
                        produto.PrecoUnitario = produtoBody.PrecoUnitario != 0.0 ? produtoBody.PrecoUnitario : produto.PrecoUnitario;

                        _database.SaveChanges();
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

        [HttpDelete("{id}")]
        public IActionResult DeleteProduto(int id)
        {
            try {
                Produto produto = _database.Produtos.First(p => p.Id == id);
                produto.Status = false;
                _database.SaveChanges();
                return Ok(new {msg = "Produto removido com Sucesso!"});

            } catch (Exception) {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Produto não encontrado!" });
            }
        }
    }
}