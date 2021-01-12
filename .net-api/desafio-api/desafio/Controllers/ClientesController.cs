using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CryptSharp;
using desafio.Data;
using desafio.HATEOAS;
using desafio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace desafio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly DataContext Database;
        private Hateoas HATEOAS;
        private readonly IMapper Mapper;

        public ClientesController(DataContext database, IMapper mapper)
        {
            this.Mapper = mapper;
            this.Database = database;
            HATEOAS = new Hateoas("localhost:5001/api/clientes");
            HATEOAS.AddAction("get_cliente", "GET");
            HATEOAS.AddAction("edit_cliente", "PATCH");
            HATEOAS.AddAction("update_cliente", "PUT");
            HATEOAS.AddAction("delete_cliente", "DELETE");
        }



        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var clientes = Database.Clientes.Where(c => c.Status == true).ToList();
                List<ClienteHATEOAS> clientesHATEOAS = new List<ClienteHATEOAS>();
                foreach (var cliente in clientes)
                {
                    ClienteHATEOAS clienteHATEOAS = new ClienteHATEOAS();
                    clienteHATEOAS.cliente = cliente;
                    clienteHATEOAS.links = HATEOAS.GetActions(cliente.Id.ToString());
                    clientesHATEOAS.Add(clienteHATEOAS);
                }
                return Ok(clientesHATEOAS);
            }
            catch (Exception e)
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Nenhum Cliente encontrado!", erro = e.Message });
            }
        }

        [HttpGet("asc")]
        public IActionResult GetByNomeAscending()
        {
            try
            {
                var clientes = Database.Clientes.Where(c => c.Status == true).OrderBy(c => c.Nome).ToList();
                List<ClienteHATEOAS> clientesHATEOAS = new List<ClienteHATEOAS>();
                foreach (var cliente in clientes)
                {
                    ClienteHATEOAS clienteHATEOAS = new ClienteHATEOAS();
                    clienteHATEOAS.cliente = cliente;
                    clienteHATEOAS.links = HATEOAS.GetActions(cliente.Id.ToString());
                    clientesHATEOAS.Add(clienteHATEOAS);
                }
                return Ok(clientesHATEOAS);
            }
            catch (Exception e)
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Nenhum Cliente encontrado!", erro = e.Message });
            }
        }

        [HttpGet("desc")]
        public IActionResult GetByNomeDescending()
        {
            try
            {
                var clientes = Database.Clientes.Where(c => c.Status == true).OrderByDescending(c => c.Nome).ToList();
                List<ClienteHATEOAS> clientesHATEOAS = new List<ClienteHATEOAS>();
                foreach (var cliente in clientes)
                {
                    ClienteHATEOAS clienteHATEOAS = new ClienteHATEOAS();
                    clienteHATEOAS.cliente = cliente;
                    clienteHATEOAS.links = HATEOAS.GetActions(cliente.Id.ToString());
                    clientesHATEOAS.Add(clienteHATEOAS);
                }
                return Ok(clientesHATEOAS);
            }
            catch (Exception e)
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Nenhum Cliente encontrado!", erro = e.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var cliente = Database.Clientes.Where(c => c.Status == true).First(c => c.Id == id);
                ClienteHATEOAS clienteHATEOAS = new ClienteHATEOAS();
                clienteHATEOAS.cliente = cliente;
                clienteHATEOAS.links = HATEOAS.GetActions(cliente.Id.ToString());
                return Ok(clienteHATEOAS);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return new ObjectResult(new { msg = $"Cliente com Id {id} não encontrado!", erro = e.Message });
            }
        }

        [HttpGet("nome/{nome}")]
        public IActionResult GetByName(string nome)
        {
            try
            {
                var cliente = Database.Clientes.Where(c => c.Status == true).First(c => c.Nome.Contains(nome));
                ClienteHATEOAS clienteHATEOAS = new ClienteHATEOAS();
                clienteHATEOAS.cliente = cliente;
                clienteHATEOAS.links = HATEOAS.GetActions(cliente.Id.ToString());
                return Ok(clienteHATEOAS);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return new ObjectResult(new { msg = $"cliente com nome {nome} não encontrado!", erro = e.Message });
            }
        }



        /// <summary>
        /// Método responsável por cadastrar um Cliente, insira apenas o nome, email, senha e documento, os outros campos serão gerados automaticamente.
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] Cliente cliente)
        {

            if (cliente.Nome.Length <= 1 || String.IsNullOrEmpty(cliente.Nome) || String.IsNullOrWhiteSpace(cliente.Nome))
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "Nome do cliente Nulo ou Inválido" });
            }
            if (cliente.Email == null || String.IsNullOrEmpty(cliente.Email) || String.IsNullOrWhiteSpace(cliente.Email))
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "Email do Cliente Nulo ou Inválido!" });
            }
            if (cliente.Senha == null || String.IsNullOrEmpty(cliente.Senha) || String.IsNullOrWhiteSpace(cliente.Senha) || cliente.Senha.Length <= 8)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "Senha do Cliente Nula, Inválida ou menor que 8 caracteres!" });
            }
            if (cliente.Documento == null || String.IsNullOrEmpty(cliente.Documento) || String.IsNullOrWhiteSpace(cliente.Documento) || cliente.Documento.Length <= 8)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "Documento do Cliente Nulo ou Inválido!" });
            }

            cliente.Senha = Crypter.Sha256.Crypt(cliente.Senha);
            cliente.DataCadastro = DateTime.Now;
            cliente.Status = true;

            Database.Add(cliente);
            Database.SaveChanges();
            Response.StatusCode = 201;
            return new ObjectResult(new { msg = "cliente Cadastrado com Sucesso!", cliente });
        }



        /// <summary>
        /// Método responsável por atualizar completamente um Cliente, insira apenas o nome, email, senha e documento, os outros campos não podem ser atualizados.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Cliente clienteBody)
        {
            try
            {
                if (clienteBody.Nome.Length <= 1 || String.IsNullOrEmpty(clienteBody.Nome) || String.IsNullOrWhiteSpace(clienteBody.Nome))
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "Nome do cliente Nulo ou Inválido" });
                }
                if (clienteBody.Email == null || String.IsNullOrEmpty(clienteBody.Email) || String.IsNullOrWhiteSpace(clienteBody.Email))
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "Email do Cliente Nulo ou Inválido!" });
                }
                if (String.IsNullOrEmpty(clienteBody.Senha) || String.IsNullOrWhiteSpace(clienteBody.Senha) || clienteBody.Senha.Length <= 8)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "Senha do Cliente Nula, Inválida ou menor que 8 caracteres!" });
                }
                if (clienteBody.Documento == null || String.IsNullOrEmpty(clienteBody.Documento) || String.IsNullOrWhiteSpace(clienteBody.Documento) || clienteBody.Documento.Length <= 8)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "Documento do Cliente Nulo ou Inválido!" });
                }

                Cliente cliente = Database.Clientes.Where(c => c.Status == true).First(c => c.Id == id);
                if (cliente == null) return BadRequest("cliente não encontrado!");
                cliente.Nome = clienteBody.Nome;
                cliente.Email = clienteBody.Email;
                cliente.Senha = Crypter.Sha256.Crypt(clienteBody.Senha);
                cliente.Documento = clienteBody.Documento;

                Database.SaveChanges();
                Response.StatusCode = 201;
                return new ObjectResult(new { msg = "cliente Atualizado com Sucesso!", cliente });
            }
            catch (Exception e)
            {
                Response.StatusCode = 401;
                return new ObjectResult(new { msg = "Falha ao Atualizar cliente!", erro = e.Message });
            }
        }



        /// <summary>
        /// Método responsável por atualizar parcialmente um Cliente, insira opcionalmente o nome, email, senha e documento, os outros campos não podem ser atualizados.
        /// </summary>
        [HttpPatch("{id}")]
        public IActionResult PatchOrActive(int id, [FromBody] Cliente clienteBody)
        {
            if (id > 0)
            {
                try
                {
                    var cliente = Database.Clientes.First(f => f.Id == id);
                    if (cliente == null) return BadRequest("Cliente não encontrado!");

                    if (cliente != null)
                    {
                        cliente.Nome = clienteBody.Nome != null ? clienteBody.Nome : cliente.Nome;
                        cliente.Email = clienteBody.Email != null ? clienteBody.Email : cliente.Email;
                        cliente.Senha = clienteBody.Senha != null ? Crypter.Sha256.Crypt(clienteBody.Senha) : cliente.Senha;
                        cliente.Documento = clienteBody.Documento != null ? clienteBody.Documento : cliente.Documento;

                        Database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new { msg = "Cliente atualizado com Sucesso!", cliente });
                    }
                    else
                    {
                        Response.StatusCode = 404;
                        return new ObjectResult(new { msg = "Cliente não encontrado!" });
                    }
                }
                catch (Exception e)
                {
                    Response.StatusCode = 404;
                    return new ObjectResult(new { msg = $"Cliente com id {id} não encontrado!", e.Message });
                }
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Id do Cliente Inválido" });
            }
        }



        /// <summary>
        /// Método responsável por remover um cliente, mas não se preocupe, ele não será apagado do banco, apenas desativado, pois ele pode estar sendo usado nos registros de vendas.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                try
                {
                    var cliente = Database.Clientes.Where(f => f.Status == true).First(f => f.Id.Equals(id));
                    if (cliente == null) return BadRequest("Cliente não encontrado!");

                    cliente.Status = false;
                    Database.SaveChanges();
                    Response.StatusCode = 200;
                    return new ObjectResult(new { msg = "Cliente removido com Sucesso!" });

                }
                catch (Exception e)
                {
                    Response.StatusCode = 401;
                    return new ObjectResult(new { msg = "Falha ao Remover Cliente!", erro = e });
                }
            }
            else
            {
                Response.StatusCode = 401;
                return new ObjectResult(new { msg = "Falha ao Remover Cliente, Id Inválido !" });
            }
        }
    }
}