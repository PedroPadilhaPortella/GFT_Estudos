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
        private readonly ApplicationDbContext Database; //injeção de dependencia do banco de dados
        public BibliotecaController(ApplicationDbContext database){
            this.Database = database;
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
            if(livro.Id == 0){
                Database.Livros.Add(livro);
            }else{
                Livro livroDB = Database.Livros.First(lib => lib.Id == livro.Id);
                livroDB.Titulo = livro.Titulo;
                livroDB.Autor = livro.Autor;
                livroDB.QuantidadeDePaginas = livro.QuantidadeDePaginas;
                livroDB.QuantidadeDeExemplares = livro.QuantidadeDeExemplares;
            }
            Database.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int Id)
        {
            Livro livro = Database.Livros.First(lib => lib.Id == Id);
            return View("Cadastrar", livro);
        }

        public IActionResult Excluir(int Id)
        {
            Livro livro = Database.Livros.First(lib => lib.Id == Id);
            Database.Livros.Remove(livro);
            Database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
