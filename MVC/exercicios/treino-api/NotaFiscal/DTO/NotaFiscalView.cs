using System;
using System.Collections.Generic;
using NotaFiscal.Models;

namespace NotaFiscal.DTO
{
    public class NotaFiscalView
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public DateTime DataEmissao { get; set; }
        public Cliente Cliente { get; set; }
        public ICollection<ProdutoNotaFiscal> ProdutosNotaFiscal { get; set; }
    }
}