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
    public class FornecedoresController : ControllerBase
    {
        private readonly DataContext Database;
        private readonly IMapper Mapper;

        public FornecedoresController(DataContext database, IMapper mapper)
        {
            this.Mapper = mapper;
            Database = database;
        }

        [HttpGet]
        public IActionResult Get() {
            try {
                var fornecedores = Database.Fornecedores.Where(f => f.Status == true).Include(f => f.Produtos).ToList();
                return Ok(Mapper.Map<IEnumerable<FornecedorDTO>>(fornecedores));
            }catch (Exception e) {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Nenhum Fornecedor encontrado!", erro = e.Message });
            }
        }

        [HttpGet("asc")]
        public IActionResult GetOrderByNomeAscending() {
            try {
                var fornecedores = Database.Fornecedores.Where(f => f.Status == true).Include(f => f.Produtos).OrderBy(f => f.Nome).ToList();
                return Ok(Mapper.Map<IEnumerable<FornecedorDTO>>(fornecedores));
            }catch (Exception e) {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Nenhum Fornecedor encontrado!", erro = e.Message });
            }
        }

        [HttpGet("desc")]
        public IActionResult GetOrderByNomeDescending() {
            try {
                var fornecedores = Database.Fornecedores.Where(f => f.Status == true).Include(f => f.Produtos).OrderByDescending(f => f.Nome).ToList();
                return Ok(Mapper.Map<IEnumerable<FornecedorDTO>>(fornecedores));
            }catch (Exception e) {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Nenhum Fornecedor encontrado!", erro = e.Message });
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var fornecedor = Database.Fornecedores.Where(f => f.Status == true).Include(f => f.Produtos).First(f => f.Id.Equals(id));
                return Ok(Mapper.Map<FornecedorDTO>(fornecedor));
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
                var fornecedor = Database.Fornecedores.Where(f => f.Status == true).Include(f => f.Produtos).First(f => f.Nome.Contains(nome));
                return Ok(Mapper.Map<FornecedorDTO>(fornecedor));
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return new ObjectResult(new { msg = $"Fornecedor com nome {nome} não encontrado!", erro = e.Message });
            }
        }


        [HttpPost]
        public IActionResult Post([FromBody] Fornecedor fornecedor)
        {

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

                Fornecedor fornecedor = Database.Fornecedores.Where(f => f.Status == true).First(f => f.Id == id);
                if (fornecedor == null) return BadRequest("Fornecedor não encontrado!");
                fornecedor.Nome = fornecedorBody.Nome;
                fornecedor.CNPJ = fornecedorBody.CNPJ;

                Database.SaveChanges();
                Response.StatusCode = 201;
                return new ObjectResult(new { msg = "Fornecedor Atualizado com Sucesso!", fornecedor });
            }
            catch (Exception e)
            {
                Response.StatusCode = 401;
                return new ObjectResult(new { msg = "Falha ao Atualizar Fornecedor!", erro = e.Message });
            }
        }


        [HttpPatch("{id}")]
        public IActionResult PatchOrActive(int id, [FromBody] Fornecedor fornecedorBody)
        {
            if (id > 0)
            {
                try
                {
                    var fornecedor = Database.Fornecedores.First(f => f.Id == id);
                    if (fornecedor == null) return BadRequest("Fornecedor não encontrado!");

                    if (fornecedor != null)
                    {
                        fornecedor.Nome = fornecedorBody.Nome != null ? fornecedorBody.Nome : fornecedor.Nome;
                        fornecedor.CNPJ = fornecedorBody.CNPJ != null ? fornecedorBody.CNPJ : fornecedor.CNPJ;

                        Database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new { msg = "Fornecedor atualizado com Sucesso!", fornecedor });
                    }
                    else
                    {
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


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                try
                {
                    var fornecedor = Database.Fornecedores.Where(f => f.Status == true).First(f => f.Id.Equals(id));
                    if (fornecedor == null) return BadRequest("Fornecedor não encontrado!");

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