using System;
using System.Collections.Generic;

namespace NotaFiscal.Models
{
    public class NotaFiscal
    {
        public int Id { get; set; }
        public DateTime DataEmissao { get; set; }
        public double Valor { get; set; }
        public Cliente Cliente { get; set; }
        public ICollection<ProdutoNotaFiscal> ProdutosNotaFiscal { get; set; }

        public NotaFiscal() { }
        public NotaFiscal(int id, DateTime dataEmissao, double valor, Cliente cliente)
        {
            this.Id = id;
            this.DataEmissao = dataEmissao; 
            this.Valor = valor;
            this.Cliente = cliente;
        }
    }
}