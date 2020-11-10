using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FuncionariosWA.DTO
{
    public class CargoDTO
    {
        [Required]
        public int Id { get; set; }


        [Required(ErrorMessage = "O Nome do Cargo é Obrigatório.")]
        public string Nome { get; set; }
    }
}