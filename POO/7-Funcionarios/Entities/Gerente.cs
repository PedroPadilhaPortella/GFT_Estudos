using System;

namespace _7_Funcionarios.Entities
{
    class Gerente : Funcionario
    {
        public Gerente(string nome, int idade, double salario) : base(nome, idade, salario){}
        public override double Bonificacao()
        {
            return Salario + 10000.00;
        }
    }
}