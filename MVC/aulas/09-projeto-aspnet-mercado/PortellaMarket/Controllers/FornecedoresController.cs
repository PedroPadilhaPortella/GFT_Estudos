using Microsoft.AspNetCore.Mvc;
using PortellaMarket.Data;
using PortellaMarket.DTO;
using PortellaMarket.Models;
using System;
using System.Linq;

namespace PortellaMarket.Controllers
{
    public class FornecedoresController : Controller
    {
        private readonly ApplicationDbContext Database;

        public FornecedoresController(ApplicationDbContext database){
            Database = database;
        }

        [HttpPost]
        public IActionResult Salvar(FornecedorDTO fornecedorTemporario) {
            if(ModelState.IsValid){
                Fornecedor fornecedor = new Fornecedor();
                fornecedor.Nome = fornecedorTemporario.Nome;
                fornecedor.Email = fornecedorTemporario.Email;
                fornecedor.Telefone = fornecedorTemporario.Telefone;
                fornecedor.Status = true;
                Database.Fornecedores.Add(fornecedor);
                Database.SaveChanges();
                return RedirectToAction("Fornecedores", "Gestao");
            }else{
                return View("../Gestao/NovoFornecedor");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(FornecedorDTO fornecedorTemporario){
            if(ModelState.IsValid){
                var fornecedor = Database.Fornecedores.First(f => f.Id == fornecedorTemporario.Id);
                fornecedor.Nome = fornecedorTemporario.Nome;
                fornecedor.Email = fornecedorTemporario.Email;
                fornecedor.Telefone = fornecedorTemporario.Telefone;
                Database.SaveChanges();
                return RedirectToAction("Fornecedores", "Gestao");
            }else{
                return View("../Gestao/EditarFornecedores");
            }
        }

        public IActionResult Excluir(int id){
            if(id > 0){
                var fornecedor = Database.Fornecedores.First(f => f.Id == id);
                fornecedor.Status = false;
                Database.SaveChanges();
            }
            return RedirectToAction("Fornecedores", "Gestao");
        }
    }
}