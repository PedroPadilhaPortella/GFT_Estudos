using System.ComponentModel.DataAnnotations;

namespace FuncionariosWA.DTO
{
    public class FuncionarioTecnologiaDTO
    {
        public int Id { get; set; }

        [Required]
        public int FuncionarioId { get; set; }

        [Required]
        public int TecnologiaId { get; set; }
    }
}