using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FuncionariosWA.Models;
using FuncionariosWA.Data;
using Microsoft.EntityFrameworkCore;
using FuncionariosWA.DTO;

namespace FuncionariosWA.Controllers
{
    public class WaController : Controller
    {
        private readonly ApplicationDbContext Database;

        public WaController(ApplicationDbContext database){
            Database = database;
        }

        [HttpGet("")]
        public IActionResult Wa()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

//Actions de Cargos //
        public IActionResult Cargos()
        {
            var cargos = Database.Cargos.Where(c => c.Status == true).ToList();
            return View(cargos);
        }
        
//Actions de Funcionarios //
        public IActionResult Funcionarios()
        {
            var funcionario = Database.Funcionarios.Where(f => f.Status == true).Include(f => f.Cargo).Include(f => f.LocalDeTrabalho).Include(f => f.Tecnologia).ToList();
            return View(funcionario);
        }

//Actions de Locais de Trabalho //
        public IActionResult LocaisDeTrabalho()
        {
            var locaisDeTrabalho = Database.LocaisDeTrabalho.Where(gft => gft.Status == true).ToList();
            return View(locaisDeTrabalho);
        }

//Actions de Tecnologias //
        public IActionResult Tecnologias()
        {
            var tecnologia = Database.Tecnologias.Where(t => t.Status == true).ToList();
            return View(tecnologia);
        }

//Actions de Vagas //
        public IActionResult Vagas()
        {
            var vaga = Database.Vagas.Where(v => v.Status == true).Include(v => v.Tecnologia).Include(v => v.Cargo).ToList();
            return View(vaga);
        }

//Actions de Alocacao
        public IActionResult Alocacao()
        {
            ViewBag.Vagas = Database.Vagas.Where(n => n.Status == true).ToList();
            ViewBag.Funcionarios = Database.Funcionarios.Where(n => n.Status == true).ToList();
            
            return View();
        }

        public IActionResult SalvarAlocacao(AlocacaoDTO alocacaoT)
        {
            if(ModelState.IsValid){
                Alocacao alocacao = new Alocacao();
                alocacao.Vagas = Database.Vagas.First(v => v.Id == alocacaoT.VagaId);
                alocacao.Funcionarios = Database.Funcionarios.First(f => f.Id == alocacaoT.FuncionarioId);
                alocacao.Data = DateTime.Now;

                var FuncionarioAlocado = Database.Funcionarios.First(f => f.Id == alocacaoT.FuncionarioId);
                var VagaAlocada = Database.Vagas.First(v => v.Id == alocacaoT.VagaId);
                FuncionarioAlocado.Status = false;
                VagaAlocada.QuantidadeDeVagas -= 1;

                if(VagaAlocada.QuantidadeDeVagas <= 0) 
                    VagaAlocada.Status = false;

                Database.Alocacao.Add(alocacao);
                Database.SaveChanges();
                return RedirectToAction("Historico", "Wa");
            }else{
                ViewBag.Vagas = Database.Vagas.Where(n => n.Status == true).ToList();
                ViewBag.Funcionarios = Database.Funcionarios.Where(n => n.Status == true).ToList();

                return View("../Wa/Alocacao");
            }
        }

        public IActionResult Historico()
        {
            var alocacoes = Database.Alocacao.Include(a => a.Funcionarios).Include(a => a.Vagas).ToList();
            return View(alocacoes);
        }
    }
}
