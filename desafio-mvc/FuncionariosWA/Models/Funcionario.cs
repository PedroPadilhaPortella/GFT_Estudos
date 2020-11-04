using System;
using System.Collections.Generic;

namespace FuncionariosWA.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula  { get; set; }
        public string Cargo { get; set; }
        public DateTime Inicio_wa { get; set; }
        public DateTime Termino_wa { get; set; }
        public int AlocacaoId { get; set; }
        public GFT Local_de_Trabalho { get; set; }
        public ICollection<FuncionarioTecnologia> FuncionarioTecnologias { get; set; }
        public bool Status { get; set; }
    }
}