namespace FuncionariosWA.Models
{
    public class LocalDeTrabalho
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Telefone { get; set; }
        public bool Status { get; set; }

        public LocalDeTrabalho(){}
        public LocalDeTrabalho(int id, string nome, string cep, string endereco, string cidade, string estado, string telefone, bool status){
            Id = id;
            Nome = nome;
            Cep = cep;
            Endereco = endereco;
            Cidade = cidade;
            Estado = estado;
            Telefone = telefone;
            Status = status;
        }
    }
}
