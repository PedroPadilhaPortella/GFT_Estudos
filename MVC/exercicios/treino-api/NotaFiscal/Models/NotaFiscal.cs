using System;
using System.Collections.Generic;

namespace NotaFiscal.Models
{
    public class NotaFiscal
    {
        public int Id { get; set; }
        public DateTime DataEmissao { get; set; }
        public float Valor { get; set; }
        public Cliente Cliente { get; set; }
        public ICollection<ProdutoNotaFiscal> ProdutosNotaFiscal { get; set; }
    }
}