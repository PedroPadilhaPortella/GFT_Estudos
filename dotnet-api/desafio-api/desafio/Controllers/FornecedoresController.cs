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
    public class FornecedoresController : ControllerBase
    {
        private readonly DataContext Database;
        private readonly IMapper Mapper;
        private Hateoas HATEOAS;

        public FornecedoresController(DataContext database, IMapper mapper)
        {
            this.Mapper = mapper;
            Database = database;
            HATEOAS = new Hateoas("localhost:5001/api/fornecedores");
            HATEOAS.AddAction("get_fornecedor", "GET");
            HATEOAS.AddAction("edit_fornecedor", "PATCH");
            HATEOAS.AddAction("update_fornecedor", "PUT");
            HATEOAS.AddAction("delete_fornecedor", "DELETE");
        }



        [HttpGet]
        public IActionResult Get() {
            try {
                var fornecedoresDB = Database.Fornecedores.Where(f => f.Status == true).Include(f => f.Produtos).ToList();
                var fornecedores = Mapper.Map<IEnumerable<FornecedorDTO>>(fornecedoresDB);
                List<FornecedorHATEOAS> fornecedoresHATEOAS = new List<FornecedorHATEOAS>();
                foreach (var fornecedor in fornecedores)
                {
                    FornecedorHATEOAS fornecedorHATEOAS = new FornecedorHATEOAS();
                    fornecedorHATEOAS.fornecedor = fornecedor;
                    fornecedorHATEOAS.links = HATEOAS.GetActions(fornecedor.Id.ToString());
                    fornecedoresHATEOAS.Add(fornecedorHATEOAS);
                }
                return Ok(fornecedoresHATEOAS);
            }
            catch (Exception e) {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Nenhum Fornecedor encontrado!", erro = e.Message });
            }
        }


        [HttpGet("asc")]
        public IActionResult GetOrderByNomeAscending() {
            try {
                var fornecedoresDB = Database.Fornecedores.Where(f => f.Status == true).Include(f => f.Produtos).OrderBy(f => f.Nome).ToList();
                var fornecedores = Mapper.Map<IEnumerable<FornecedorDTO>>(fornecedoresDB);
                List<FornecedorHATEOAS> fornecedoresHATEOAS = new List<FornecedorHATEOAS>();
                foreach (var fornecedor in fornecedores)
                {
                    FornecedorHATEOAS fornecedorHATEOAS = new FornecedorHATEOAS();
                    fornecedorHATEOAS.fornecedor = fornecedor;
                    fornecedorHATEOAS.links = HATEOAS.GetActions(fornecedor.Id.ToString());
                    fornecedoresHATEOAS.Add(fornecedorHATEOAS);
                }
                return Ok(fornecedoresHATEOAS);
            }
            catch (Exception e) {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Nenhum Fornecedor encontrado!", erro = e.Message });
            }
        }


        [HttpGet("desc")]
        public IActionResult GetOrderByNomeDescending() {
            try {
                var fornecedoresDB = Database.Fornecedores.Where(f => f.Status == true).Include(f => f.Produtos).OrderByDescending(f => f.Nome).ToList();
                var fornecedores = Mapper.Map<IEnumerable<FornecedorDTO>>(fornecedoresDB);
                List<FornecedorHATEOAS> fornecedoresHATEOAS = new List<FornecedorHATEOAS>();
                foreach (var fornecedor in fornecedores)
                {
                    FornecedorHATEOAS fornecedorHATEOAS = new FornecedorHATEOAS();
                    fornecedorHATEOAS.fornecedor = fornecedor;
                    fornecedorHATEOAS.links = HATEOAS.GetActions(fornecedor.Id.ToString());
                    fornecedoresHATEOAS.Add(fornecedorHATEOAS);
                }
                return Ok(fornecedoresHATEOAS);
            }
            catch (Exception e) {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Nenhum Fornecedor encontrado!", erro = e.Message });
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var fornecedorDB = Database.Fornecedores.Where(f => f.Status == true).Include(f => f.Produtos).First(f => f.Id.Equals(id));
                var fornecedor = Mapper.Map<FornecedorDTO>(fornecedorDB);
                FornecedorHATEOAS fornecedorHATEOAS = new FornecedorHATEOAS();
                fornecedorHATEOAS.fornecedor = fornecedor;
                fornecedorHATEOAS.links = HATEOAS.GetActions(fornecedor.Id.ToString());
                return Ok(fornecedorHATEOAS);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return new ObjectResult(new { msg = $"Fornecedor com Id {id} não encontrado!", erro = e.Message });
            }
        }


        [HttpGet("nome/{nome}")]
        public IActionResult GetByName(string nome)
        {
            try
            {
                var fornecedorDB = Database.Fornecedores.Where(f => f.Status == true).Include(f => f.Produtos).First(f => f.Nome.Contains(nome));
                var fornecedor = Mapper.Map<FornecedorDTO>(fornecedorDB);
                FornecedorHATEOAS fornecedorHATEOAS = new FornecedorHATEOAS();
                fornecedorHATEOAS.fornecedor = fornecedor;
                fornecedorHATEOAS.links = HATEOAS.GetActions(fornecedor.Id.ToString());
                return Ok(fornecedorHATEOAS);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return new ObjectResult(new { msg = $"Fornecedor com nome {nome} não encontrado!", erro = e.Message });
            }
        }




        /// <summary>
        /// Método responsável por cadastrar um Fornecedor, insira apenas o nome e cnpj, os outros campos serão gerados automaticamente.
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] Fornecedor fornecedor)
        {
            try{
                if (fornecedor.Nome.Length <= 1 || String.IsNullOrEmpty(fornecedor.Nome) || String.IsNullOrWhiteSpace(fornecedor.Nome))
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "Nome do Fornecedor Nulo ou Inválido" });
                }
                if (fornecedor.CNPJ == null || String.IsNullOrEmpty(fornecedor.CNPJ) || String.IsNullOrWhiteSpace(fornecedor.CNPJ))
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "CNPJ do Fornecedor Nulo ou Inválido!" });
                }
                fornecedor.Status = true;

                Database.Add(fornecedor);
                Database.SaveChanges();
                Response.StatusCode = 201;
                return new ObjectResult(new { msg = "Fornecedor Cadastrado com Sucesso!", fornecedor });
            }
            catch(Exception e) {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "Falha ao Cadastrar fornecedor!", e.Message });
            }
        }



        /// <summary>
        /// Método responsável por atualizar completamente um Fornecedor, insira apenas o nome e cnpj, os outros campos não podem ser atualizados.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Fornecedor fornecedorBody)
        {
            try
            {
                if (fornecedorBody.Nome.Length <= 1 || String.IsNullOrEmpty(fornecedorBody.Nome) || String.IsNullOrWhiteSpace(fornecedorBody.Nome))
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "O Nome do Fornecedor precisa ter mais de 5 caractere" });
                }
                if (fornecedorBody.CNPJ.Length <= 15 || String.IsNullOrEmpty(fornecedorBody.CNPJ) || String.IsNullOrWhiteSpace(fornecedorBody.CNPJ))
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "CNPJ do Fornecedor Nulo ou Inválido" });
                }

                Fornecedor fornecedor = new Fornecedor();
                try {
                    fornecedor = Database.Fornecedores.First(f => f.Id == id);
                } catch(Exception e) {
                    Response.StatusCode = 404;
                    return new ObjectResult(new { msg = "Fornecedor não encontrado!", e.Message });
                }
                fornecedor.Nome = fornecedorBody.Nome;
                fornecedor.CNPJ = fornecedorBody.CNPJ;

                Database.SaveChanges();
                Response.StatusCode = 201;
                return new ObjectResult(new { msg = "Fornecedor Atualizado com Sucesso!", fornecedor });
            }
            catch (Exception e)
            {
                Response.StatusCode = 401;
                return new ObjectResult(new { msg = "Falha ao Atualizar Fornecedor, verifique os campos!", erro = e.Message });
            }
        }



        /// <summary>
        /// Método responsável por atualizar parcialmente um Fornecedor, insira opcionalmente o nome e cnpj, os outros campos não podem ser atualizados.
        /// </summary>
        [HttpPatch("{id}")]
        public IActionResult PatchOrActive(int id, [FromBody] Fornecedor fornecedorBody)
        {
            if (id > 0)
            {
                try{
                    Fornecedor fornecedor = new Fornecedor();
                    try {
                        fornecedor = Database.Fornecedores.First(f => f.Id == id);
                    }catch(Exception e) {
                        Response.StatusCode = 404;
                        return new ObjectResult(new { msg = "Fornecedor não encontrado!", e.Message });
                    }

                    if (fornecedor != null) {
                        fornecedor.Nome = fornecedorBody.Nome != null ? fornecedorBody.Nome : fornecedor.Nome;
                        fornecedor.CNPJ = fornecedorBody.CNPJ != null ? fornecedorBody.CNPJ : fornecedor.CNPJ;

                        Database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new { msg = "Fornecedor atualizado com Sucesso!", fornecedor });
                    }else{
                        Response.StatusCode = 404;
                        return new ObjectResult(new { msg = "Fornecedor não encontrado!" });
                    }
                }
                catch (Exception e)
                {
                    Response.StatusCode = 404;
                    return new ObjectResult(new { msg = $"Fornecedor com id {id} não encontrado!", e.Message });
                }
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Id do Fornecedor Inválido" });
            }
        }



        /// <summary>
        /// Método responsável por remover um Fornecedor, mas não se preocupe, ele não será apagado do banco, apenas desativado, pois ele pode estar sendo usado nos registros de produtos.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                try
                {
                    Fornecedor fornecedor = new Fornecedor();
                    try {
                        fornecedor = Database.Fornecedores.First(f => f.Id == id);
                    } catch(Exception e) {
                        Response.StatusCode = 404;
                        return new ObjectResult(new { msg = "Fornecedor não encontrado!", e.Message });
                    }

                    fornecedor.Status = false;
                    Database.SaveChanges();
                    Response.StatusCode = 200;
                    return new ObjectResult(new { msg = "Fornecedor removido com Sucesso!" });
                }
                catch (Exception e)
                {
                    Response.StatusCode = 401;
                    return new ObjectResult(new { msg = "Falha ao Remover Fornecedor!", erro = e });
                }
            }
            else
            {
                Response.StatusCode = 401;
                return new ObjectResult(new { msg = "Falha ao Remover Fornecedor, Id Inválido !" });
            }
        }
    }
}