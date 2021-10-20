using System;
using FuncionariosWAClean.Domain.Validations;

namespace FuncionariosWAClean.Domain.Entities
{
    public class Alocacao : Entity
    {
        public DateTime Data { get; private set; }
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
        public int VagaId { get; set; }
        public Vaga Vaga { get; set; }

        public Alocacao(int id, DateTime data)
        {
            DomainExceptionValidation.When(id < 0, "Id Inválido");
            Id = id;
            Data = data;
        }

        public Alocacao(DateTime data)
        {
            Data = data;
        }
    }
}