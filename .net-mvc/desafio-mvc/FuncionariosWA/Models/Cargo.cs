using System;
using System.Collections.Generic;

namespace FuncionariosWA.Models
{
    public class Cargo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
        
        public Cargo(){ }
        public Cargo(int id, string nome, bool status) {
            Id = id;
            Nome = nome;
            Status = status;
        }
    }
}