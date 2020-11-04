using System;
using System.Collections.Generic;

namespace FuncionariosWA.Models
{
    public class Vaga
    {
        public int Id { get; set; }
        public string Projeto { get; set; }
        public string Descricao { get; set; }
        public string CodigoVaga { get; set; }
        public DateTime AberturaVaga { get; set; }
        public int QuantidadeVagas { get; set; }
        public ICollection<VagaTecnologia> VagaTecnologias  { get; set; }
        public bool Status { get; set; }
    }
}