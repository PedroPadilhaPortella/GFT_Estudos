using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotaFiscal.Data;
using NotaFiscal.DTO;
using NotaFiscal.HATEOAS;
using NotaFiscal.Models;

namespace NotaFiscal.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Authorize(Roles = "Admin,Employee")]
    public class NotaFiscalController : ControllerBase
    {
        private readonly NotaFiscalContext Database;
        private Hateoas HATEOAS;
        public NotaFiscalController(NotaFiscalContext database)
        {
            this.Database = database;

            HATEOAS = new Hateoas("localhost:5001/v1/NotaFiscal");
            HATEOAS.AddAction("get_notafiscal", "GET");
            HATEOAS.AddAction("edit_notafiscal", "PATCH");
        }

        [HttpGet]
        public IActionResult GetNotaFiscal(){
            try{
                var notasFiscais = Database.NotasFiscais.Include(n => n.Cliente).Include(n => n.ProdutosNotaFiscal).ThenInclude(n => n.Produto).ToList();

                List<NotaFiscalH> notasfiscaisHATEOAS = new List<NotaFiscalH>();

                foreach(var notaFiscal in notasFiscais){
                    NotaFiscalH notafiscalHATEOAS = new NotaFiscalH();
                    notafiscalHATEOAS.notaFiscal = notaFiscal;
                    notafiscalHATEOAS.links = HATEOAS.GetActions(notaFiscal.Id.ToString()); 
                    notasfiscaisHATEOAS.Add(notafiscalHATEOAS); 
                }

                return Ok(notasfiscaisHATEOAS);
            } catch(Exception e) {
                return BadRequest(new {msg = e});
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetNotaFiscalById(int id){
            if(id > 0){
                try {
                    var notaFiscal = Database.NotasFiscais.Include(n => n.Cliente).Include(n => n.ProdutosNotaFiscal).ThenInclude(n => n.Produto).First(N => N.Id == id);
                    NotaFiscalH notaFiscalHATEOAS = new NotaFiscalH();
                    notaFiscalHATEOAS.notaFiscal = notaFiscal;
                    notaFiscalHATEOAS.links = HATEOAS.GetActions(notaFiscal.Id.ToString());

                    return Ok(notaFiscalHATEOAS);
                } catch(Exception e) {
                    Response.StatusCode = 404;
                    return new ObjectResult(new {msg = $"Nota Fiscal com Id {id} não encontrado!", log = e});
                }
            }else{
                    Response.StatusCode = 404;
                    return new ObjectResult(new {msg = $"Nota Fiscal com Id {id} não encontrado!"});
            }
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