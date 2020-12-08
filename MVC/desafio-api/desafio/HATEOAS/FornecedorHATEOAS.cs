using desafio.DTO;

namespace desafio.HATEOAS
{
    public class FornecedorHATEOAS
    {
        public FornecedorDTO fornecedor { get; set; }
        public Link[] links { get; set; }
    }
}