using System;
using System.Collections.Generic;
using NotaFiscal.Models;

namespace NotaFiscal.DTO
{
    public class NotaFiscalView
    {
        public int Id { get; set; }
        public float Valor { get; set; }
        public DateTime DataEmissao { get; set; }
        public Cliente Cliente { get; set; }
        public ICollection<ProdutoNotaFiscal> ProdutosNotaFiscal { get; set; }
    }
}