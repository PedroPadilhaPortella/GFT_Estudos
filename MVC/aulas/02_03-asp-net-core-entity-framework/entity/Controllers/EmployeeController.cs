using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using entity.Models;
using entity.Database;

namespace entity.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly ApplicationDBContext Database; //readonly significa que só pode ser alterado aqui e no construtor
        public FuncionariosController(ApplicationDBContext database) {
        //Injeção de dependencia do objeto ApplicationDBContext na Controller
            this.Database = database;
        }

        public IActionResult Index(){
            var funcionarios = Database.Funcionarios.ToList();
            return View(funcionarios);
        }

        public IActionResult Cadastrar(){
            return View();
        }

        //o parametro int id já é padrão e opcional nas rotas asp.net, por isso não tem necessidade de passar argumentos via HttpGet("url") 
        public IActionResult Editar(int id){
            Funcionario funcionario = Database.Funcionarios.First(func => func.Id == id); //busca uma query no banco de dados a partir do id
            return View("Cadastrar", funcionario);
        }
        public IActionResult Excluir(int id){
            Funcionario funcionario = Database.Funcionarios.First(func => func.Id == id); //busca uma query no banco de dados a partir do id
            Database.Funcionarios.Remove(funcionario);
            Database.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Salvar(Funcionario funcionario){
            if(funcionario.Id == 0){
                //adicionar novo funcionario
                Database.Funcionarios.Add(funcionario);
            }else{
                //atualizar cadastro do funcionario
                Funcionario funcionarioDB = Database.Funcionarios.First(func => func.Id == funcionario.Id);
                //modificacoes diretas na variavel funcionarioDB vem do banco de dados e suas alterações são aplicadas no banco de dados 
                funcionarioDB.Nome = funcionario.Nome;
                funcionarioDB.Salario = funcionario.Salario;
                funcionarioDB.Cpf = funcionario.Cpf;
            }
            Database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}