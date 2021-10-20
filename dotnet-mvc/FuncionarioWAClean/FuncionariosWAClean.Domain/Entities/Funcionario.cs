using System;
using System.Collections.Generic;
using FuncionariosWAClean.Domain.Validations;

namespace FuncionariosWAClean.Domain.Entities
{
    public class Funcionario : Entity
    {
        public string Nome { get; private set; }
        public string Matricula { get; private set; }
        public DateTime InicioWa { get; private set; }
        public DateTime TerminoWa { get; private set; }
        public bool Status { get; private set; }
        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }
        public int LocalDeTrabalhoId { get; set; }
        public LocalDeTrabalho LocalDeTrabalho { get; set; }
        public ICollection<FuncionarioTecnologia> FuncionarioTecnologias { get; set; }

        public Funcionario(int id, string nome, string matricula, bool status) 
        {
            ValidateDomain(nome, matricula);
            DomainExceptionValidation.When(id < 0, "Id Inválido");
            Id = id;
            InicioWa = DateTime.Now;
            TerminoWa = DateTime.Now.AddDays(15);
            Status = status;
        }

        public Funcionario(string nome, string matricula, bool status) 
        {
            ValidateDomain(nome, matricula);
            InicioWa = DateTime.Now;
            TerminoWa = DateTime.Now.AddDays(15);
            Status = status;
        }

        public void Update(string nome, string matricula, bool status)
        {
            ValidateDomain(nome, matricula);
            Status = status;
        }

        private void ValidateDomain(string nome, string matricula)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "O Campo Nome é requerido!");
            DomainExceptionValidation.When(nome.Length < 2, "O Campo Nome precisa ter no mínimo 2 caracteres!");

            Nome = nome;
            Matricula = matricula;
        }
    }
}