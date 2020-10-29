using System;
using System.Globalization;

namespace _03_Conta
{
    abstract class Conta
    {

        protected string Nome { get; set; }
        protected string Titular { get; set; }
        protected double Saldo { get; set; }

        public abstract double Rendimento();

        public override string ToString()
        {
            return $"Nome: {Nome}\nTitular: {Titular}\nSaldo: R$ {Saldo.ToString("F2", CultureInfo.InvariantCulture)}";
        }
    }
}