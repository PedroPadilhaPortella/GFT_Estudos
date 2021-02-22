using Newtonsoft.Json;

namespace desafio.DTO
{
    public class ProdutoAuxiliar
    {
        public int Id { get; set; }
        [JsonIgnore]
        public string Nome { get; set; }
        [JsonIgnore]
        public string Codigo { get; set; }
        [JsonIgnore]
        public double Valor { get; set; }
        [JsonIgnore]
        public bool Promocao { get; set; }
        [JsonIgnore]
        public double ValorPromocao { get; set; }
        [JsonIgnore]
        public string Categoria { get; set; }
        [JsonIgnore]
        public string Imagem { get; set; }
        [JsonIgnore]
        public int Quantidade { get; set; }
        [JsonIgnore]
        public int FornecedorId { get; set; }
    }
}