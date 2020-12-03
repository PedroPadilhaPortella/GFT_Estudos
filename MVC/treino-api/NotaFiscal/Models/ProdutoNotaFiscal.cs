using Newtonsoft.Json;

namespace NotaFiscal.Models
{
    public class ProdutoNotaFiscal
    {
        public int Quantidade { get; set; }

        [JsonIgnore]
        public int NotaFiscalId { get; set; }

        [JsonIgnore]
        public int ProdutoId { get; set; }
        public NotaFiscal NotaFiscal { get; set; }
        public Produto Produto { get; set; }

        public ProdutoNotaFiscal() { }
        public ProdutoNotaFiscal(int quantidade, NotaFiscal notaFiscal, Produto produto)
        {
            this.Quantidade = quantidade;
            this.NotaFiscal = notaFiscal;
            this.Produto = produto;
        }
    }
}