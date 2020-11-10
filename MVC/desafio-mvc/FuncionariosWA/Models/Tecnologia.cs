using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FuncionariosWA.Models
{
    public class Tecnologia
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }

        public Tecnologia() { }
        public Tecnologia(int id, string nome, bool status){
            Id = id;
            Nome = nome;
            Status = status;
        }
    }
}