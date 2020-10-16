using System;

namespace _7_Funcionarios.Entities
{
    class Vendedor : Funcionario
    {
        public Vendedor(string nome, int idade, double salario) : base(nome, idade, salario){}
        public override double Bonificacao()
        {
            return Salario + 3000.00;
        }
    }
}