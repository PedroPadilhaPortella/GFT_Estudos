using System;

namespace _03_Conta
{
    class ContaPoupanca : Conta, IImposto
    {
        public ContaPoupanca(string nome, string titular, double saldo)
        {
            Nome = nome;
            Titular = titular;
            Saldo = saldo;
        }

        public override double Rendimento(){
            double Rendimento = Saldo * 0.05;
            Saldo += Rendimento;
            return Rendimento;
        }

        public double CalcularImposto(){
            return Rendimento() * 0.1;
        }
    }
}