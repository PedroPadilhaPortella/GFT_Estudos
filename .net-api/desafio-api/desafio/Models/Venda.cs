using System;
using System.Collections.Generic;

namespace desafio.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public Cliente Cliente { get; set; }
        public double Total { get; set; }
        public DateTime DataVenda { get; set; }
        public ICollection<ProdutoVenda> ProdutosVenda { get; set; }

        public Venda() { }
        public Venda(int id, Fornecedor fornecedor, Cliente cliente, double total, DateTime dataVenda)
        {
            this.Id = id;
            this.Fornecedor = fornecedor;
            this.Cliente = cliente;
            this.Total = total;
            this.DataVenda = dataVenda;
        }
    }
}