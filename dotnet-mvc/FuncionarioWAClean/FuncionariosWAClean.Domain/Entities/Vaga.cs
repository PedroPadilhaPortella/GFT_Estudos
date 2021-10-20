using System;
using System.Collections.Generic;
using FuncionariosWAClean.Domain.Validations;

namespace FuncionariosWAClean.Domain.Entities
{
    public class Vaga : Entity
    {
        public string Projeto { get; private set; }
        public string Descricao { get; private set; }
        public string CodigoDaVaga { get; private set; }
        public int QuantidadeDeVagas { get; private set; }
        public DateTime AberturaDaVaga { get; private set; }
        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }
        public bool Status { get; set; }
        public ICollection<VagaTecnologia> VagaTecnologias { get; set; }

        public Vaga(int id, string projeto, string descricao, string codigoDaVaga, int quantidadeDeVagas, DateTime aberturaDaVaga, int cargoId, bool status)
        {
            ValidateDomain(projeto, descricao, codigoDaVaga, quantidadeDeVagas);
            DomainExceptionValidation.When(id < 0, "Id Inválido");
            Id = id;
            AberturaDaVaga = aberturaDaVaga;
            CargoId = cargoId;
            Status = status;
        }

        public Vaga(string projeto, string descricao, string codigoDaVaga, int quantidadeDeVagas, DateTime aberturaDaVaga, bool status)
        {
            ValidateDomain(projeto, descricao, codigoDaVaga, quantidadeDeVagas);
            AberturaDaVaga = aberturaDaVaga;
            Status = status;
        }

        public void Update(string projeto, string descricao, string codigoDaVaga, int quantidadeDeVagas, DateTime aberturaDaVaga, int cargoId, bool status)
        {
            ValidateDomain(projeto, descricao, codigoDaVaga, quantidadeDeVagas);
            AberturaDaVaga = aberturaDaVaga;
            CargoId = cargoId;
            Status = status;
        }


        private void ValidateDomain(string projeto, string descricao, string codigoDaVaga, int quantidadeDeVagas)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(projeto), "O Nome do Projeto é requerido!");
            DomainExceptionValidation.When(string.IsNullOrEmpty(descricao), "A Descrição do Projeto é requerido!");
            DomainExceptionValidation.When(string.IsNullOrEmpty(codigoDaVaga), "O Código da Vaga é requerido!");
            DomainExceptionValidation.When(quantidadeDeVagas > 0, "A Quantidade de Vagas precisa ser maior que 0");
            Projeto = projeto;
            Descricao = descricao;
            CodigoDaVaga = codigoDaVaga;
            QuantidadeDeVagas = quantidadeDeVagas;
        }
    }
}