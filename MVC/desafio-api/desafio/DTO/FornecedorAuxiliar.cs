using System.Collections.Generic;
using Newtonsoft.Json;

namespace desafio.DTO
{
    public class FornecedorAuxiliar
    {
        public int Id { get; set; }
        // [JsonIgnore]
        public string Nome { get; set; }
        [JsonIgnore]
        public string CNPJ { get; set; }
        [JsonIgnore]
        public bool Status { get; set; }
        [JsonIgnore]
        public List<ProdutoDTO> Produtos { get; set; }
    }
}