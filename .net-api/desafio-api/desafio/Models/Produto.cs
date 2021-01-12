using System.Collections.Generic;

namespace desafio.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public double Valor { get; set; }
        public bool Promocao { get; set; }
        public double ValorPromocao { get; set; }
        public string Categoria { get; set; }
        public string Imagem { get; set; }
        public int Quantidade { get; set; }
        public bool Status { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public ICollection<ProdutoVenda> ProdutosVenda { get; set; }
        public Produto() { }
        public Produto(int id, string nome, string codigo, double valor, bool promocao, double valorPromocao,
            string categoria, int quantidade, Fornecedor fornecedor)
        {
            this.Id = id;
            this.Nome = nome;
            this.Codigo = codigo;
            this.Valor = valor;
            this.Promocao = promocao;
            this.ValorPromocao = valorPromocao;
            this.Categoria = categoria;
            this.Imagem = $"{nome}.jpg".Replace(" ", "-");
            this.Quantidade = quantidade;
            this.Fornecedor = fornecedor;
            this.Status = true;
        }
    }
}