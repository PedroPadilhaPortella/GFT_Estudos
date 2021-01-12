using System;
using System.Collections.Generic;
using desafio.Models;
using Newtonsoft.Json;

namespace desafio.DTO
{
    public class VendaDTO
    {
        public int Id { get; set; }
        [JsonIgnore]
        public Fornecedor Fornecedor { get; set; }
        [JsonIgnore]
        public Cliente Cliente { get; set; }
        public int ClienteId { get; set; }
        public int FornecedorId { get; set; }
        public double Total { get; set; }
        public DateTime DataVenda { get; set; }
        public ProdutoVendaDTO[] ProdutosVenda  { get; set; }
        [JsonIgnore]
        public List<ProdutoVendaDTO> ProdutosVendaDTO  { get; set; }
    }
}