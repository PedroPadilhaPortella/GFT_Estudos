using System.Collections.Generic;
using Newtonsoft.Json;

namespace NotaFiscal.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double PrecoUnitario { get; set; }

        [JsonIgnore]
        public bool Status { get; set; }
        
        [JsonIgnore]
        public ICollection<ProdutoNotaFiscal> ProdutosNotaFiscal { get; set; }

        public Produto() { }
        public Produto(int id, string nome, double preco, bool status){
            this.Id = id;
            this.Nome= nome;
            this.PrecoUnitario = preco;
            this.Status = status;
        }
    }
}