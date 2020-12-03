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

                Database.Vagas.Add(vaga);

                foreach(var id in vagaT.TecnologiaId){
                    VagaTecnologia vagaTecnologia = new VagaTecnologia();
                    vagaTecnologia.Vaga = vaga;
                    vagaTecnologia.Tecnologia = Database.Tecnologias.First(t => t.Id == id);
                    Database.Add(vagaTecnologia);
                }

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
            var vaga = Database.Vagas.Include(v => v.Cargo).First(v => v.Id == id);
            VagaDTO vagaView = new VagaDTO();
            vagaView.Id = vaga.Id;
            vagaView.Projeto = vaga.Projeto;
            vagaView.Descricao = vaga.Descricao;
            vagaView.CodigoDaVaga = vaga.CodigoDaVaga;
            vagaView.QuantidadeDeVagas = vaga.QuantidadeDeVagas;
            vagaView.CargoId = vaga.Cargo.Id;

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

                var removerRelacao = Database.VagaTecnologias.Where(vt => vt.VagaId == vagaT.Id).ToList();
                foreach(var e in removerRelacao){
                    Database.Remove(e);
                }
                Database.RemoveRange(removerRelacao);

                foreach(var id in vagaT.TecnologiaId){
                    VagaTecnologia vagaTecnologia = new VagaTecnologia();
                    vagaTecnologia.Vaga = vaga;
                    vagaTecnologia.Tecnologia = Database.Tecnologias.First(t => t.Id == id);
                    Database.Add(vagaTecnologia);
                }

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
                Vaga vaga = Database.Vagas.First(v => v.Id == id);
                vaga.Status = false;
                Database.SaveChanges();
            }
            return RedirectToAction("Vagas", "Wa");
        }
    }
}