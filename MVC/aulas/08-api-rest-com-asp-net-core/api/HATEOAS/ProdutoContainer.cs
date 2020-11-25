using api.Models;

namespace api.HATEOAS
{

    public class ProdutoContainer
    {
        public Produto produto { get; set; }
        public Link[] links { get; set; }
    }
}