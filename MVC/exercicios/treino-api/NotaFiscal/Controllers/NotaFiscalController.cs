using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotaFiscal.Data;
using NotaFiscal.DTO;
using NotaFiscal.Models;

namespace NotaFiscal.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class NotaFiscalController : ControllerBase
    {
        private readonly NotaFiscalContext Database;
        public NotaFiscalController(NotaFiscalContext database)
        {
            this.Database = database;
        }

        [HttpGet]
        public IActionResult GetNotaFiscal(){
            try{
                var notaFiscal = Database.NotasFiscais
                .Include(n => n.Cliente)
                .Include(n => n.ProdutosNotaFiscal).ThenInclude(n => n.Produto)
                .ToList();

                List<NotaFiscalView> dataList = new List<NotaFiscalView>();
                foreach(var nota in notaFiscal) {
                    NotaFiscalView data = new NotaFiscalView();
                    data.Id = nota.Id;
                    data.Valor = nota.Valor;
                    data.DataEmissao = nota.DataEmissao;
                    data.Cliente = nota.Cliente;
                    data.ProdutosNotaFiscal = nota.ProdutosNotaFiscal;
                    dataList.Add(data);
                }

                return Ok(notaFiscal.ToArray());
            } catch(Exception e) {
                return BadRequest(new {msg = e});
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetNotaFiscalById(int id){
            return Ok("ok");
        }

        [HttpPost]
        public IActionResult PostNotaFiscal([FromBody] NotaFiscalDTO notaFiscalDTO)
        {
            try {

                if(notaFiscalDTO.Valor <= 0) {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "O Total não pode ser menor que 0.0"});
                }

                Cliente cliente = new Cliente();
                cliente = Database.Clientes.First(c => c.Id == notaFiscalDTO.ClienteId);

                if(cliente == null){
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = $"Cliente com Id {notaFiscalDTO.ClienteId} não encontrado!"});
                }

                NotaFiscal.Models.NotaFiscal nota = new NotaFiscal.Models.NotaFiscal();
                nota.Valor = notaFiscalDTO.Valor;
                nota.DataEmissao = DateTime.Now;
                nota.Cliente = cliente;
                Database.NotasFiscais.Add(nota);
                
                foreach(var produtoNF in notaFiscalDTO.ProdutosNotaFiscalDTO){
                    ProdutoNotaFiscal produtonota = new ProdutoNotaFiscal();
                    produtonota.Quantidade = produtoNF.Quantidade;
                    produtonota.Produto = Database.Produtos.First(p => p.Id == produtoNF.ProdutoId);
                    produtonota.NotaFiscal = nota;
                    Database.ProdutosNotaFiscal.Add(produtonota);
                }

                Database.SaveChanges();

                Response.StatusCode = 201;
                return new ObjectResult(new { msg = "Nota Fiscal salva com Sucesso!" });

            } catch {
                Response.StatusCode = 401;
                return new ObjectResult(new {msg = "Falha ao Salvar os Dados!"});
            }
        }

        [HttpPatch]
        public IActionResult PatchNotaFiscal([FromBody] NotaFiscalDTO notaFiscalDTO) {
            if(notaFiscalDTO.Id > 0) {

                try {
                    var notafiscal = Database.NotasFiscais.First(p => p.Id.Equals(notaFiscalDTO.Id));

                    if(notafiscal != null) {

                        Cliente cliente = new Cliente();
                        cliente = Database.Clientes.First(c => c.Id.Equals(notaFiscalDTO.ClienteId));

                        if(cliente == null){
                            Response.StatusCode = 404;
                            return new ObjectResult(new {msg = $"Cliente com Id {notaFiscalDTO.ClienteId} não encontrado!"});
                        }

                        notafiscal.Valor = notaFiscalDTO.Valor != 0.0 ? notaFiscalDTO.Valor : notafiscal.Valor;
                        notafiscal.Cliente = cliente;

                        var removerRelacao = Database.ProdutosNotaFiscal.Where(n => n.NotaFiscalId.Equals(notafiscal.Id)).ToList();
                        foreach(var e in removerRelacao){
                            Database.Remove(e);
                        }
                        Database.RemoveRange(removerRelacao);

                        foreach(var produtoNF in notaFiscalDTO.ProdutosNotaFiscalDTO){
                            ProdutoNotaFiscal produtonota = new ProdutoNotaFiscal();
                            produtonota.Quantidade = produtoNF.Quantidade;
                            produtonota.Produto = Database.Produtos.First(p => p.Id == produtoNF.ProdutoId);
                            produtonota.NotaFiscal = notafiscal;
                            Database.ProdutosNotaFiscal.Add(produtonota);
                        }

                        Database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new { msg = "Nota Fiscal atualizada com Sucesso!" });
                    }else {
                        Response.StatusCode = 404;
                        return new ObjectResult(new { msg = "Nota Fiscal não encontrada!" });
                    }
                } catch(Exception e) {
                    Response.StatusCode = 404;
                    return new ObjectResult(new { msg = $"NotaFiscal com Id {notaFiscalDTO.Id} não encontrado!", erro = e });
                }
            
            }else {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Id do Produto Inválido" });
            }
        }
    }
}