using System;
using System.ComponentModel.DataAnnotations;

namespace FuncionariosWA.Models
{
    public class Alocacao
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }

        [Required]
        public Funcionario Funcionarios { get; set; }


        [Required]
        public Vaga Vagas { get; set; }
    }
}