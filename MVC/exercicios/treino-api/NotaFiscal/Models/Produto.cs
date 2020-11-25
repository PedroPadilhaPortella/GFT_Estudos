using System.Collections.Generic;
using Newtonsoft.Json;

namespace NotaFiscal.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public float PrecoUnitario { get; set; }

        [JsonIgnore]
        public bool Status { get; set; }
        
        [JsonIgnore]
        public ICollection<ProdutoNotaFiscal> ProdutosNotaFiscal { get; set; }
    }
}