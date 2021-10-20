using FuncionariosWAClean.Domain.Validations;

namespace FuncionariosWAClean.Domain.Entities
{
    public class LocalDeTrabalho : Entity
    {
        public string Nome { get; private set; }
        public string Cep { get; private set; }
        public string Endereco { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string Telefone { get; private set; }
        public bool Status { get; private set; }

        public LocalDeTrabalho(int id, string nome, string cep, string endereco, string cidade, string estado, string telefone, bool status){
            ValidateDomain(nome, cep, endereco, cidade, estado, telefone, status);
            DomainExceptionValidation.When(id < 0, "Id Inválido");
            Id = id;
        }

        public LocalDeTrabalho(string nome, string cep, string endereco, string cidade, string estado, string telefone, bool status)
        {
            ValidateDomain(nome, cep, endereco, cidade, estado, telefone, status);
        }

        public void Update(string nome, string cep, string endereco, string cidade, string estado, string telefone, bool status)
        {
            ValidateDomain(nome, cep, endereco, cidade, estado, telefone, status);
        }

        private void ValidateDomain(string nome, string cep, string endereco, string cidade, string estado, string telefone, bool status)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "O Campo Nome é requerido!");
            DomainExceptionValidation.When(string.IsNullOrEmpty(cep), "O Campo CEP é requerido!");
            DomainExceptionValidation.When(cep.Length < 8, "O Campo CEP precisa ter no mínimo 8 caracteres!");
            DomainExceptionValidation.When(string.IsNullOrEmpty(endereco), "O Campo Endereco é requerido!");
            DomainExceptionValidation.When(string.IsNullOrEmpty(cidade), "O Campo Cidade é requerido!");
            DomainExceptionValidation.When(string.IsNullOrEmpty(estado), "O Campo Estado é requerido!");
            DomainExceptionValidation.When(string.IsNullOrEmpty(telefone), "O Campo Telefone é requerido!");

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
