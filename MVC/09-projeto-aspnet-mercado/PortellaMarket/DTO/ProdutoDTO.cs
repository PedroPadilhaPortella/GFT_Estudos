using System.ComponentModel.DataAnnotations;

namespace PortellaMarket.DTO
{
    public class ProdutoDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome do Produto é Obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do Produto é muito extenso, tente um nome mais Objetivo.")]
        [MinLength(2, ErrorMessage = "O nome do Produto é muito curto, tente um nome mais Descritivo.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Categoria é Obrigatória.")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Fornecedor é Obrigatório.")]
        public int FornecedorId { get; set; }

        [Required(ErrorMessage = "O Preco de Custo é Obrigatório.")]
        public float PrecoDeCusto { get; set; }

        [Required(ErrorMessage = "O Preco de Custo é Obrigatório.")]
        public string PrecoDeCustoString { get; set; }

        [Required(ErrorMessage = "O Preco de Venda é Obrigatório.")]
        public float PrecoDeVenda { get; set; }

        [Required(ErrorMessage = "O Preco de Venda é Obrigatório.")]
        public string PrecoDeVendaString { get; set; }

        [Required(ErrorMessage = "Medição do Produto é Obrigatória")]
        [Range(0, 2, ErrorMessage = "Unidade de Medida Inválida.")]
        public int Medicao { get; set; }
    }
}