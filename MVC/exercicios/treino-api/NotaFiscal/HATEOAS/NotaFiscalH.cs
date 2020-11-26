using NotaFiscal.Models;

namespace NotaFiscal.HATEOAS
{
    public class NotaFiscalH
    {
        public NotaFiscal.Models.NotaFiscal notaFiscal { get; set; }
        public Link[] links { get; set; }
    }
}