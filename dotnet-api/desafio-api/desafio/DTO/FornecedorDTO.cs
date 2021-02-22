using System.Collections.Generic;
using Newtonsoft.Json;

namespace desafio.DTO
{
    public class FornecedorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        [JsonIgnore]
        public bool Status { get; set; }
        public List<ProdutoAuxiliar> Produtos { get; set; }
    }
}