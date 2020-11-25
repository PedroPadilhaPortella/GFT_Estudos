using System;
using System.Collections.Generic;
using NotaFiscal.Models;

namespace NotaFiscal.DTO
{
    public class NotaFiscalDTO
    {
        public int Id { get; set; }
        public float Valor { get; set; }
        public DateTime DataEmissao { get; set; }
        public int ClienteId { get; set; }
        public ProdutoNotaFiscalDTO[] ProdutosNotaFiscalDTO  { get; set; }
    }
}