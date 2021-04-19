using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;
using Biblioteca.Database;

namespace Biblioteca.Controllers
{
    public class BibliotecaController : Controller
    {
        private readonly ILivroRepository Repository;

        public BibliotecaController(ILivroRepository repository)
        {
            this.Repository = repository;
        }


        public IActionResult Index()
        {
            var livros = Repository.getAll();
            return View(livros);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Salvar(Livro livro)
        {
            Repository.Add(livro);
            return RedirectToAction("Index", "Biblioteca");
        }

        public IActionResult Editar(int Id)
        {
            Livro livro = Repository.getById(Id);
            return View(livro);
        }

        public IActionResult Atualizar(Livro livro)
        {
            Repository.Update(livro);
            return RedirectToAction("Index", "Biblioteca");
        }

        public IActionResult Excluir(int Id)
        {
            Repository.RemoveById(Id);
            return RedirectToAction("Index");
        }

        public IActionResult SeedData()
        {
            Repository.SeedData();
            return RedirectToAction("Index");
        }
    }
}
