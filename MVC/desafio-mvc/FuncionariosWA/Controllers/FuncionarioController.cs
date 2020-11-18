using System;
using System.Linq;
using FuncionariosWA.Data;
using FuncionariosWA.DTO;
using FuncionariosWA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FuncionariosWA.Controllers
{
    [Authorize]
    public class FuncionarioController : Controller
    {
        private readonly ApplicationDbContext Database;

        public FuncionarioController(ApplicationDbContext database)
        {
            Database = database;
        }

        public IActionResult Novo()
        {
            ViewBag.LocaisDeTrabalho = Database.LocaisDeTrabalho.Where(n => n.Status == true).ToList();
            ViewBag.Cargos = Database.Cargos.Where(n => n.Status == true).ToList();
            ViewBag.Tecnologias = Database.Tecnologias.Where(n => n.Status == true).ToList();

            return View();
        }

        public IActionResult Salvar(FuncionarioDTO functionarioT)
        {
            if (ModelState.IsValid)
            {
                Funcionario funcionario = new Funcionario();
                funcionario.Nome = functionarioT.Nome;
                funcionario.Matricula = functionarioT.Matricula;
                funcionario.InicioWa = DateTime.Now;
                funcionario.TerminoWa = DateTime.Now.AddDays(15);
                funcionario.Status = true;
                funcionario.Cargo = Database.Cargos.First(c => c.Id == functionarioT.CargoId);
                funcionario.LocalDeTrabalho = Database.LocaisDeTrabalho.First(l => l.Id == functionarioT.LocalDeTrabalhoId);

                Database.Funcionarios.Add(funcionario);

                foreach(var id in functionarioT.TecnologiaId){
                    FuncionarioTecnologia funcionarioTecnologia = new FuncionarioTecnologia();
                    funcionarioTecnologia.Funcionario = funcionario;
                    funcionarioTecnologia.Tecnologia = Database.Tecnologias.First(t => t.Id == id);
                    Database.Add(funcionarioTecnologia);
                }

                Database.SaveChanges();
                return RedirectToAction("Funcionarios", "Wa");
            }
            else
            {
                ViewBag.LocaisDeTrabalho = Database.LocaisDeTrabalho.Where(n => n.Status == true).ToList();
                ViewBag.Cargos = Database.Cargos.Where(n => n.Status == true).ToList();
                ViewBag.Tecnologias = Database.Tecnologias.Where(n => n.Status == true).ToList();

                return View("../Funcionario/Novo");
            }
        }

        public IActionResult Editar(int id)
        {
            var funcionario = Database.Funcionarios.Include(f => f.LocalDeTrabalho).Include(f => f.Cargo)/*.Include(f => f.Tecnologia)*/.First(f => f.Id == id);
            FuncionarioDTO funcionarioView = new FuncionarioDTO();
            funcionarioView.Id = funcionario.Id;
            funcionarioView.Nome = funcionario.Nome;
            funcionarioView.Matricula = funcionario.Matricula;
            funcionarioView.CargoId = funcionario.Cargo.Id;
            // funcionarioView.TecnologiaId = funcionario.Tecnologia.Id;
            funcionarioView.LocalDeTrabalhoId = funcionario.LocalDeTrabalho.Id;

            ViewBag.LocaisDeTrabalho = Database.LocaisDeTrabalho.Where(n => n.Status == true).ToList();
            ViewBag.Cargos = Database.Cargos.Where(n => n.Status == true).ToList();
            ViewBag.Tecnologias = Database.Tecnologias.Where(n => n.Status == true).ToList();

            return View(funcionarioView);
        }

        public IActionResult Atualizar(FuncionarioDTO funcionarioT)
        {
            if (ModelState.IsValid)
            {
                var funcionario = Database.Funcionarios.First(f => f.Id == funcionarioT.Id);
                funcionario.Nome = funcionarioT.Nome;
                funcionario.Matricula = funcionarioT.Matricula;
                funcionario.Cargo = Database.Cargos.First(c => c.Id == funcionarioT.CargoId);
                funcionario.LocalDeTrabalho = Database.LocaisDeTrabalho.First(l => l.Id == funcionarioT.LocalDeTrabalhoId);
                
                var removerRelacao = Database.FuncionarioTecnologias.Where(ft => ft.FuncionarioId == funcionarioT.Id).ToList();
                foreach(var e in removerRelacao){
                    Database.Remove(e);
                }
                Database.RemoveRange(removerRelacao);

                foreach(var id in funcionarioT.TecnologiaId){
                    FuncionarioTecnologia funcionarioTecnologia = new FuncionarioTecnologia();
                    funcionarioTecnologia.Funcionario = funcionario;
                    funcionarioTecnologia.Tecnologia = Database.Tecnologias.First(t => t.Id == id);
                    Database.Add(funcionarioTecnologia);
                }

                Database.SaveChanges();
                return RedirectToAction("Funcionarios", "Wa");
            }
            else
            {
                ViewBag.LocaisDeTrabalho = Database.LocaisDeTrabalho.Where(n => n.Status == true).ToList();
                ViewBag.Cargos = Database.Cargos.Where(n => n.Status == true).ToList();
                ViewBag.Tecnologias = Database.Tecnologias.Where(n => n.Status == true).ToList();

                return View("../Funcionario/Editar");
            }
        }

        public IActionResult Excluir(int id)
        {
            if (id > 0)
            {
                var funcionario = Database.Funcionarios.First(f => f.Id == id);
                funcionario.Status = false;
                Database.SaveChanges();
            }
            return RedirectToAction("Funcionarios", "Wa");
        }
    }
}