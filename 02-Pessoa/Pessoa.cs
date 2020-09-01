using System;

namespace _02_Pessoa
{
    class Pessoa{

        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }

        public Pessoa(string nome, string endereco, string telefone){
            Nome = nome;
            Endereco = endereco;
            Telefone = telefone;
        }

        public override string ToString(){
            return $"Nome: {Nome}\nEndereço: {Endereco}\nTelefone: {Telefone}";
        }
    }
}