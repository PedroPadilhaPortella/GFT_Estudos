using System;

namespace _03_Conta
{
    class ContaCorrente : Conta
    {
        public ContaCorrente(string nome, string titular, double saldo)
        {
            Nome = nome;
            Titular = titular;
            Saldo = saldo;
        }
        public override double Rendimento(){
            double Rendimento = Saldo * 0.03;
            Saldo += Rendimento;
            return Rendimento;
        }
    }
}