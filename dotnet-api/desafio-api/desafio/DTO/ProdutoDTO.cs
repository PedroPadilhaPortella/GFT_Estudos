using Newtonsoft.Json;

namespace desafio.DTO
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public double Valor { get; set; }
        public bool Promocao { get; set; }
        public double ValorPromocao { get; set; }
        public string Categoria { get; set; }
        public string Imagem { get; set; }
        public int Quantidade { get; set; }
        [JsonIgnore]
        public bool Status { get; set; }
        public int FornecedorId { get; set; }
        public FornecedorAuxiliar Fornecedor { get; set; }
    }
}