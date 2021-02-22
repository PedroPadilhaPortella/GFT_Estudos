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

        public Cliente() { }
        public Cliente(int id, string nome, string cpf, string cep, string telefone, bool status){
            this.Id = id;
            this.Nome= nome;
            this.CPF = cpf;
            this.CEP = cep;
            this.Telefone = telefone;
            this.Status = status;
        }
    }
}