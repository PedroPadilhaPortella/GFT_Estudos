using desafio.DTO;

namespace desafio.HATEOAS
{
    public class ProdutoHATEOAS
    {
        public ProdutoDTO produto { get; set; }
        public Link[] links { get; set; }
    }
}