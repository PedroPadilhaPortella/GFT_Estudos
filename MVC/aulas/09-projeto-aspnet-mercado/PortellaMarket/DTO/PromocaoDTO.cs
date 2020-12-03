using System.ComponentModel.DataAnnotations;

namespace PortellaMarket.DTO
{
    public class PromocaoDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome da Promoção é Obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da Promoção é muito extenso, tente um nome mais Objetivo.")]
        [MinLength(2, ErrorMessage = "O nome da Promoção é muito curto, tente um nome mais Descritivo.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Produto é Obrigatório.")]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "Porcentagem é Obrigatório.")]
        [Range(0, 100, ErrorMessage = "Porcentagem de Promoção excede o Limite.")]
        public float Porcentagem { get; set; }
    }
}