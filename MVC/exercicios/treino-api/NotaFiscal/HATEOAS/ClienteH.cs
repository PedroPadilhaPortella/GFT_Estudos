using NotaFiscal.Models;

namespace NotaFiscal.HATEOAS
{
    public class ClienteH
    {
        public Cliente cliente { get; set; }
        public Link[] links { get; set; }
    }
}