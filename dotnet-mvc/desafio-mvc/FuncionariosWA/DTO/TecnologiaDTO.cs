using System.ComponentModel.DataAnnotations;

namespace FuncionariosWA.DTO
{
    public class TecnologiaDTO
    {
        [Required]
        public int Id { get; set; }


        [Required(ErrorMessage = "O Nome da Tecnologia é Obrigatório.")]
        public string Nome { get; set; }
    }
}