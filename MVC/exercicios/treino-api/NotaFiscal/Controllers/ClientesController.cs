using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NotaFiscal.Data;
using NotaFiscal.DTO;
using NotaFiscal.Models;

namespace NotaFiscal.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly NotaFiscalContext _database;

        public ClientesController(NotaFiscalContext database)
        {
            this._database = database;
        }

        [HttpGet]
        public IActionResult GetCliente(){
            var clientes = _database.Clientes.ToList();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public IActionResult GetClienteById(int id){
            try {
                Cliente cliente = _database.Clientes.First(c => c.Id == id);
                return Ok(cliente);
            } catch (Exception) {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Cliente não encontrado!" });
            }
        }

        [HttpPost]
        public IActionResult PostCliente([FromBody] ClienteDTO clienteDTO){

            if(clienteDTO.Nome.Length <= 1 || String.IsNullOrEmpty(clienteDTO.Nome) || String.IsNullOrWhiteSpace(clienteDTO.Nome)) {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Nome do Cliente Nulo ou Inválido"});
            }
            if(clienteDTO.CPF == null || clienteDTO.CPF.Length < 10 || String.IsNullOrEmpty(clienteDTO.CPF) || String.IsNullOrWhiteSpace(clienteDTO.CPF)) {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "CPF do Cliente Nulo ou Inválido!"});
            }
            if(clienteDTO.CEP == null || clienteDTO.CEP.Length <= 7 || String.IsNullOrEmpty(clienteDTO.CEP) || String.IsNullOrWhiteSpace(clienteDTO.CEP)) {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "CEP do Cliente Nulo ou Inválido!"});
            }
            if(clienteDTO.Telefone == null || clienteDTO.Telefone.Length < 10 || String.IsNullOrEmpty(clienteDTO.Telefone) || String.IsNullOrWhiteSpace(clienteDTO.Telefone)) {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Telefone do Cliente Nulo ou Inválido!"});
            }

            Cliente cliente = new Cliente();
            cliente.Nome = clienteDTO.Nome;
            cliente.CPF = clienteDTO.CPF;
            cliente.CEP = clienteDTO.CEP;
            cliente.Telefone = clienteDTO.Telefone;
            cliente.Status = true;

            _database.Add(cliente);
            _database.SaveChanges();
            Response.StatusCode = 201;
            return new ObjectResult(new { msg = "Cliente Cadastrado com Sucesso!", cliente });
        }

        [HttpPatch]
        public IActionResult EditarCliente([FromBody] Cliente clienteBody){
            if(clienteBody.Id > 0){
                try {
                    var cliente = _database.Clientes.First(c => c.Id == clienteBody.Id);

                    if(cliente != null) {
                        cliente.Nome = clienteBody.Nome != null ? clienteBody.Nome : cliente.Nome;
                        cliente.CPF = clienteBody.CPF != null ? clienteBody.CPF : cliente.CPF;
                        cliente.CEP = clienteBody.CEP != null ? clienteBody.CEP : cliente.CEP;
                        cliente.Telefone = clienteBody.Telefone != null ? clienteBody.Telefone : cliente.Telefone;

                        _database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new { msg = "Cliente atualizado com Sucesso!", cliente });
                    }else {
                        Response.StatusCode = 404;
                        return new ObjectResult(new { msg = "Cliente não encontrado!" });
                    }
                }catch(Exception) {
                    Response.StatusCode = 404;
                    return new ObjectResult(new { msg = $"Cliente com id {clienteBody.Id} não encontrado!" });
                }
            }else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg = "Id do Cliente Inválido!" });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirCliente(int id){
            if(id > 0){
                try{
                    Cliente cliente = _database.Clientes.First(c => c.Id == id);
                    cliente.Status = false;
                    _database.SaveChanges();

                    Response.StatusCode = 200;
                    return new ObjectResult(new {msg = "Cliente excluído com Sucesso!"});
                }catch(Exception){
                    Response.StatusCode = 404;
                    return new ObjectResult(new {msg = "Cliente não encontrado!"});
                }
            }else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg = "Id do Cliente Inválido!"});
            }
        }
    }
}