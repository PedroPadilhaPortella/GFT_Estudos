using System.ComponentModel.DataAnnotations;

namespace FuncionariosWA.DTO
{
    public class AlocacaoDTO
    {
        [Required]
        public int Id { get; set; }


        [Required(ErrorMessage="Uma Alocação Precisa de um Funcionário")]
        public int FuncionarioId { get; set; }


        [Required(ErrorMessage="Uma Alocação Precisa de uma Vaga")]
        public int VagaId { get; set; }
    }
}