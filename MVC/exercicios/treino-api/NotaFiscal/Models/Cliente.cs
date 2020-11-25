using Newtonsoft.Json;

namespace NotaFiscal.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string CEP { get; set; }
        public string Telefone { get; set; }

        [JsonIgnore]
        public bool Status { get; set; }
    }
}