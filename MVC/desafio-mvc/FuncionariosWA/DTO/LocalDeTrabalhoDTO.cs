using System.ComponentModel.DataAnnotations;

namespace FuncionariosWA.DTO
{
    public class LocalDeTrabalhoDTO
    {
        [Required]
        public int Id { get; set; }


        [Required(ErrorMessage = "O Nome do Local de Trabalho é Obrigatório.")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "O CEP do Local de Trabalho é Obrigatório.")]
        public string Cep { get; set; }


        [Required(ErrorMessage = "O Endereço do Local de Trabalho é Obrigatório.")]
        public string Endereco { get; set; }


        [Required(ErrorMessage = "A Cidade é Obrigatória.")]
        public string Cidade { get; set; }


        [Required(ErrorMessage = "O Estado é Obrigatória.")]
        public string Estado { get; set; }


        [Required(ErrorMessage = "O Telefone do Local de Trabalho é Obrigatório.")]
        [Phone(ErrorMessage = "Número de Telefone Inválido")]
        public string Telefone { get; set; }
    }
}
