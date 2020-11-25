using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Biblioteca.Database;

namespace Biblioteca.Controllers
{
    public class BibliotecaController : Controller
    {
        private readonly ApplicationDbContext Database;
        private readonly SeedingService Seeding;
        public BibliotecaController(ApplicationDbContext database, SeedingService seeding){
            this.Database = database;
            Seeding = seeding;
        }

        public IActionResult Index()
        {
            var livros = Database.Livros.ToList();
            return View(livros);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Salvar(Livro livro)
        {
            Livro livroDB = new Livro();
            livroDB.Titulo = livro.Titulo;
            livroDB.Autor = livro.Autor;
            livroDB.QuantidadeDePaginas = livro.QuantidadeDePaginas;
            livroDB.QuantidadeDeExemplares = livro.QuantidadeDeExemplares;
            Database.Livros.Add(livroDB);
            Database.SaveChanges();

            return RedirectToAction("Index", "Biblioteca");
        }

        public IActionResult Editar(int Id)
        {
            Livro livro = Database.Livros.First(lib => lib.Id == Id);
            return View(livro);
        }

        public IActionResult Atualizar(Livro livroTemporario)
        {
            Livro livro = Database.Livros.First(lib => lib.Id == livroTemporario.Id);
            livro.Titulo = livroTemporario.Titulo;
            livro.Autor = livroTemporario.Autor;
            livro.QuantidadeDePaginas = livroTemporario.QuantidadeDePaginas;
            livro.QuantidadeDeExemplares = livroTemporario.QuantidadeDeExemplares;
            Database.SaveChanges();

            return RedirectToAction("Index", "Biblioteca");
        }

        public IActionResult Excluir(int Id)
        {
            Livro livro = Database.Livros.First(lib => lib.Id == Id);
            Database.Livros.Remove(livro);
            Database.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult SeedData()
        {
            // Seeding.Seed();
            if(Database.Livros.Any()){
                return View(); //DB has been populated!
            }
            Database.AddRange(
                new Livro(1, "A Lenda dos Guardiões", "Kathryn Lasky", 230, 12),
                new Livro(2, "Percy Jackson e o Ladrão de Raios", "Rick Riordan", 321, 34),
                new Livro(3, "Clean Code", "Uncle Bob", 564, 3),
                new Livro(4, "Rangers A Ordem dos Arqueiros", "John Flanagan", 300, 10)
            );
            Database.SaveChanges();
            return View();
        }
    }
}
