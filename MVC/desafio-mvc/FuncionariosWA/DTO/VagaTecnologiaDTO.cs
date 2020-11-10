using System.ComponentModel.DataAnnotations;

namespace FuncionariosWA.DTO
{
    public class VagaTecnologiaDTO
    {
        
        public int Id { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage="Campo Inválido!")]
        public int VagaId { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage="Campo Inválido!")]
        public int TecnologiaId { get; set; }
    }
}