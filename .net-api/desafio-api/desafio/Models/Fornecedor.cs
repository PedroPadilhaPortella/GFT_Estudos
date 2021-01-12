using System.Collections.Generic;

namespace desafio.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public bool Status { get; set; }
        public List<Produto> Produtos { get; set; }
        public Fornecedor() { }
        public Fornecedor(int id, string nome, string cnpj, bool status)
        {
            this.Id = id;
            this.Nome = nome;
            this.CNPJ = cnpj;
            this.Status = status;
        }
    }
}