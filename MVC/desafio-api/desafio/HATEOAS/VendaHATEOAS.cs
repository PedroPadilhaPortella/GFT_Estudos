using desafio.DTO;

namespace desafio.HATEOAS
{
    public class VendaHATEOAS
    {
        public VendaDTO venda { get; set; }
        public Link[] links { get; set; }
    }
}