using System.ComponentModel.DataAnnotations;

namespace PortellaMarket.DTO
{
    public class FornecedorDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome do Fornecedor é Obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome do Fornecedor é muito extenso, tente um nome mais Objetivo.")]
        [MinLength(2, ErrorMessage = "O nome do Fornecedor é muito curto, tente um nome mais Descritivo.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Email do Fornecedor é Obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail Inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Número do Fornecedor é Obrigatório.")]
        [Phone(ErrorMessage = "Número de Telefone Inválido")]
        public string Telefone { get; set; }
    }
}