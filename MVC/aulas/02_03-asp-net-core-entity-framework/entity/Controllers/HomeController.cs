using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using entity.Models;
using entity.Database;
using Microsoft.EntityFrameworkCore;

namespace entity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext Database; //readonly significa que só pode ser alterado aqui e no construtor
        public HomeController(ApplicationDBContext database) {
        //Injeção de dependencia do objeto ApplicationDBContext na Controller
            this.Database = database;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Teste()
        {
            // Categoria c1 = new Categoria();
            // Categoria c2 = new Categoria();
            // Categoria c3 = new Categoria();
            // Categoria c4 = new Categoria();
            // Categoria c5 = new Categoria();
            // c1.Nome = "Administrador";
            // c2.Nome = "Gerente";
            // c3.Nome = "Funcionario";
            // c4.Nome = "Guest";
            // c5.Nome = "Guest";
            // List<Categoria> listaCat = new List<Categoria>();
            // listaCat.Add(c1);
            // listaCat.Add(c2);
            // listaCat.Add(c3);
            // listaCat.Add(c4);
            // listaCat.Add(c5);
            // Database.AddRange(listaCat);
            // Database.SaveChanges();

            var listaDeCategorias = Database.Categorias.Where(c => c.Nome.Equals("Guest") || c.Id % 2 == 0).ToList();
            string itens = "";
            foreach (var item in listaDeCategorias) {
                System.Console.WriteLine(item);
                itens += item + "\n";
            }
            return Content(itens);
        }

        public IActionResult Relacionamento()
        {
            // Produto p1 = new Produto();
            // p1.Nome = "Doritos";
            // p1.Categoria = Database.Categorias.First(c => c.Id == 1);
            // Produto p2 = new Produto();
            // p2.Nome = "Fandangos";
            // p2.Categoria = Database.Categorias.First(c => c.Id == 2);
            // Produto p3 = new Produto();
            // p3.Nome = "Cheetos";
            // p3.Categoria = Database.Categorias.First(c => c.Id == 2);
            // Database.Add(p1);
            // Database.Add(p2);
            // Database.Add(p3);
            // Database.SaveChanges();


             //O entiy framework não inclui automaticamente instancia de outras classes, então é preciso usar o include dessa classe
            /*var listaProdutos = Database.Produtos.Include(p => p.Categoria).ToList();

            string itens = "";
            listaProdutos.ForEach(produto =>{
                Console.WriteLine(produto);
                itens += produto + "\n";
            });*/

            //consultando vários elementos com base em um atributo, usando LazyLoading, mas evite usá-lo
            var produtosEspecificos = Database.Produtos./*Include(p => p.Categoria).*/Where(p => p.Categoria.Id == 2).ToList();
            string itens = "";
            produtosEspecificos.ForEach(p =>{
                System.Console.WriteLine(p);
                itens += p + "\n";
            });



            return Content(itens);
        }

    }
}
