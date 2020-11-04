using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FuncionariosWA.Models
{
    public class Tecnologia
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public bool Status { get; set; }
        public ICollection<FuncionarioTecnologia> FuncionarioTecnologias { get; set; }
        public ICollection<VagaTecnologia> VagaTecnologias { get; set; }
    }
}