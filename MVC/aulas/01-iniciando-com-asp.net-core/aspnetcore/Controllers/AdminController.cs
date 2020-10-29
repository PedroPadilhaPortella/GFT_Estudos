using System;
using Microsoft.AspNetCore.Mvc;

namespace estudos_mvc.Controllers
{
    [Route("Dashboard/Admin")]
    public class AdminController : Controller
    {
        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return Content("Hello World ASP.NET Core");
        }

        //Rota, passando o argumento da rota que é recebido pelo GET e o tipo del(ou sem tipo), podendo ser obrigatorio ou opcional
        [HttpGet("User/{nome}/{senha:int}")]
        public IActionResult Portella(string nome, int id) //parametros recebidos pela rota GET User
        {
            if(nome == "Pedro Portella" || nome == "Daniel Portella")
                return Content($"Admin: {nome},\nId: {id}");
            else
                return Content($"Common User: {nome},\nId: {id}");
        }

        [HttpGet("Numero/{numero:int?}")]
        public IActionResult Numero(int numero)
        {
            return Content("Numero: " + numero);
        }

        [HttpGet("Query")]
        public IActionResult Query()
        {
            var password = Request.Query["senha"];
            return Content($"Sua senha é: {password}");
        }

        [HttpGet("View")]
        public IActionResult Visualizar()
        {
            ViewData["curso"] = "ASP.NET CORE";
            ViewData["teste"] = true;
            return View("Visualizar");
        }
    }
}