using System;
using System.Collections.Generic;

namespace FuncionariosWA.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public DateTime InicioWa { get; set; }
        public DateTime TerminoWa { get; set; }
        public bool Status { get; set; }
        public Cargo Cargo { get; set; }
        public LocalDeTrabalho LocalDeTrabalho { get; set; }
        public Tecnologia Tecnologia { get; set; }
        
        
        public Funcionario() { }
        public Funcionario(int id, string nome, string matricula, DateTime inicioWa, Cargo cargo, 
        Tecnologia tecnologia, LocalDeTrabalho localDeTrabalho, bool status)
        {
            Id = id;
            Nome = nome;
            Matricula = matricula;
            InicioWa = inicioWa;
            TerminoWa = inicioWa.AddDays(15);
            Cargo = cargo;
            Tecnologia = tecnologia;
            LocalDeTrabalho = localDeTrabalho;
            Status = status;
        }    }
}