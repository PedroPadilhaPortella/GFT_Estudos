using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FuncionariosWA.DTO
{
    public class FuncionarioDTO
    {
        [Required]
        public int Id { get; set; }


        [Required(ErrorMessage = "O Nome do Funcionário é Obrigatório.")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "O Código da Matrícula do Funcionário é Obrigatório.")]
        public string Matricula { get; set; }


        [Required(ErrorMessage = "A data de iníco do Wa é Obrigatório.")]
        public DateTime InicioWa { get; set; }


        [Required(ErrorMessage = "O Local de Trabalho do Funcionário é Obrigatório.")]
        public int LocalDeTrabalhoId { get; set; }


        [Required(ErrorMessage = "O Cargo do Funcionário é Obrigatório.")]
        public int CargoId { get; set; }


        [Required(ErrorMessage = "O Cargo do Funcionário é Obrigatório.")]
        public int TecnologiaId { get; set; }
    }
}