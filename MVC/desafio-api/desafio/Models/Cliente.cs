using System;
using Newtonsoft.Json;

namespace desafio.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Documento { get; set; }
        public string NivelAcesso { get; set; }
        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; }

        public Cliente() { }
        public Cliente(int id, string nome, string email, string senha, string documento, string nivelAcesso, bool status, DateTime dataCadastro)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.Documento = documento;
            this.NivelAcesso = nivelAcesso;
            this.Status = status;
            this.DataCadastro = dataCadastro;
        }
    }
}