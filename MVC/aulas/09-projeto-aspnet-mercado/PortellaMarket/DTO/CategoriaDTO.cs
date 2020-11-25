using System.ComponentModel.DataAnnotations;

namespace PortellaMarket.DTO
{
    public class CategoriaDTO
    {
        [Required]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O Nome da Categoria é Obrigatório.")]
        [StringLength(100, ErrorMessage = "Este nome de Categoria é muito extenso, tente um nome mais Objetivo.")]
        [MinLength(2, ErrorMessage = "Este nome de Categoria é muito curto, tente um nome mais Descritivo.")]
        public string Nome { get; set; }
    }
}

//DTO é Data Tranfer Object, que é uma classe de transferencia de dados, usada para validar campos e dados antes de realmente salvar no banco de dados