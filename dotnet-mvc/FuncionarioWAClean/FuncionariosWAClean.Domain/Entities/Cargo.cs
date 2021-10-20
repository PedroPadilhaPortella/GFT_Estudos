using FuncionariosWAClean.Domain.Validations;

namespace FuncionariosWAClean.Domain.Entities
{
    public class Cargo : Entity
    {
        public string Nome { get; private set; }
        public bool Status { get; private set; }

        public Cargo(int id, string nome, bool status)
        {
            ValidateDomain(nome, status);
            DomainExceptionValidation.When(id < 0, "Id Inválido");
            this.Id = id;
        }

        public Cargo(string nome, bool status)
        {
            ValidateDomain(nome, status);
        }

        public void Update(string nome, bool status)
        {
            ValidateDomain(nome, status);
        }

        private void ValidateDomain(string nome, bool status)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "O Campo Nome é requerido!");
            DomainExceptionValidation.When(nome.Length < 2, "O Campo Nome precisa ter no mínimo 2 caracteres!");

            this.Status = status;
            this.Nome = nome;
        }
    }
}