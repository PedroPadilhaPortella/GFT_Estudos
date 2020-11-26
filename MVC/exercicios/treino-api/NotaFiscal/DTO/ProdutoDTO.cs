using NotaFiscal.HATEOAS;

namespace NotaFiscal.DTO
{
    public class ProdutoDTO
    {
        public string Nome { get; set; }
        public double PrecoUnitario { get; set; }
        public Link[] links { get; set; }
    }
}