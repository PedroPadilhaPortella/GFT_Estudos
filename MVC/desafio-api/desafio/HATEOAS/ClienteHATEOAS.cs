using desafio.Models;

namespace desafio.HATEOAS
{
    public class ClienteHATEOAS
    {
        public Cliente cliente { get; set; }
        public Link[] links { get; set; }
    }
}