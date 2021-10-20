using System;
using System.Collections.Generic;

namespace FuncionariosWA.Models
{
    public class Vaga
    {
        public int Id { get; set; }
        public string Projeto { get; set; }
        public string Descricao { get; set; }
        public string CodigoDaVaga { get; set; }
        public int QuantidadeDeVagas { get; set; }
        public DateTime AberturaDaVaga { get; set; }
        public Cargo Cargo { get; set; }
        public int CargoId { get; set; }
        public bool Status { get; set; }
        public ICollection<VagaTecnologia> VagaTecnologias { get; set; }


        public Vaga() { }
        public Vaga(int id, string projeto, string descricao, string codigoDaVaga, int quantidadeDeVagas, DateTime aberturaDaVaga, Cargo cargo, bool status)
        {
            Id = id;
            Projeto = projeto;
            Descricao = descricao;
            CodigoDaVaga = codigoDaVaga;
            QuantidadeDeVagas = quantidadeDeVagas;
            AberturaDaVaga = aberturaDaVaga;
            Cargo = cargo;
            Status = status;
        }
    }
}