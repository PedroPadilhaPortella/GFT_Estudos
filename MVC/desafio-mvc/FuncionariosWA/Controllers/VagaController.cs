using System;
using System.Linq;
using FuncionariosWA.Data;
using FuncionariosWA.DTO;
using FuncionariosWA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FuncionariosWA.Controllers
{
    public class VagaController : Controller
    {
        private readonly ApplicationDbContext Database;

        public VagaController(ApplicationDbContext database)
        {
            Database = database;
        }

        public IActionResult Novo()
        {
            ViewBag.Cargos = Database.Cargos.Where(n => n.Status == true).ToList();
            ViewBag.Tecnologias = Database.Tecnologias.Where(n => n.Status == true).ToList();

            return View();
        }

        public IActionResult Salvar(VagaDTO vagaT)
        {
            if (ModelState.IsValid)
            {
                Vaga vaga = new Vaga();
                vaga.Projeto = vagaT.Projeto;
                vaga.Descricao = vagaT.Descricao;
                vaga.QuantidadeDeVagas = vagaT.QuantidadeDeVagas;
                vaga.CodigoDaVaga = vagaT.CodigoDaVaga;
                vaga.AberturaDaVaga = DateTime.Now;
                vaga.Status = true;
                vaga.Cargo = Database.Cargos.First(c => c.Id == vagaT.CargoId);
                vaga.Tecnologia = Database.Tecnologias.First(t => t.Id == vagaT.TecnologiaId);

                Database.Vagas.Add(vaga);
                Database.SaveChanges();
                return RedirectToAction("Vagas", "Wa");
            }else{
                ViewBag.Cargos = Database.Cargos.Where(n => n.Status == true).ToList();
                ViewBag.Tecnologias = Database.Tecnologias.Where(n => n.Status == true).ToList();

                return View("../Vaga/Novo");
            }
        }

        public IActionResult Editar(int id)
        {
            var vaga = Database.Vagas.Include(v => v.Cargo).Include(v => v.Tecnologia).First(v => v.Id == id);
            VagaDTO vagaView = new VagaDTO();
            vagaView.Id = vaga.Id;
            vagaView.Projeto = vaga.Projeto;
            vagaView.Descricao = vaga.Descricao;
            vagaView.CodigoDaVaga = vaga.CodigoDaVaga;
            vagaView.QuantidadeDeVagas = vaga.QuantidadeDeVagas;
            vagaView.CargoId = vaga.Cargo.Id;
            vagaView.TecnologiaId = vaga.Tecnologia.Id;

            ViewBag.Cargos = Database.Cargos.Where(n => n.Status == true).ToList();
            ViewBag.Tecnologias = Database.Tecnologias.Where(n => n.Status == true).ToList();
            return View(vagaView);
        }

        public IActionResult Atualizar(VagaDTO vagaT)
        {
            if (ModelState.IsValid)
            {
                var vaga = Database.Vagas.First(v => v.Id == vagaT.Id);
                vaga.Projeto = vagaT.Projeto;
                vaga.Descricao = vagaT.Descricao;
                vaga.CodigoDaVaga = vagaT.CodigoDaVaga;
                vaga.QuantidadeDeVagas = vagaT.QuantidadeDeVagas;
                vaga.Cargo = Database.Cargos.First(c => c.Id == vagaT.CargoId);
                vaga.Tecnologia = Database.Tecnologias.First(t => t.Id == vagaT.TecnologiaId);

                Database.SaveChanges();
                return RedirectToAction("Vagas", "Wa");
            }else{
                ViewBag.Cargos = Database.Cargos.Where(n => n.Status == true).ToList();
                ViewBag.Tecnologias = Database.Tecnologias.Where(n => n.Status == true).ToList();

                return View("../Vaga/Editar");
            }
        }

        public IActionResult Excluir(int id)
        {
            if (id > 0)
            {
                var vaga = Database.Vagas.First(v => v.Id == id);
                vaga.Status = false;
                Database.SaveChanges();
            }
            return RedirectToAction("Vagas", "Wa");
        }
    }
}