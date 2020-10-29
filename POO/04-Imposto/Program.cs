using System;
using System.Globalization;

namespace _03_Conta
{
    class Program
    {
        static void Main(string[] args)
        {
            ContaCorrente conta01 = new ContaCorrente("Pedro Portella", "Pedro", 1000.00);
            ContaPoupanca conta02 = new ContaPoupanca("Edwin Portella", "Edwin", 1000.00);

            System.Console.WriteLine(conta01);
            System.Console.WriteLine(conta02);

            System.Console.WriteLine("Rendimento Conta Corrente: R$ " + conta01.Rendimento().ToString("F2", CultureInfo.InvariantCulture));
            System.Console.WriteLine("Rendimento Conta Poupanca: R$ " + conta02.Rendimento().ToString("F2", CultureInfo.InvariantCulture));

            System.Console.WriteLine("Imposto sobre Conta Corrente: R$ " + conta01.CalcularImposto().ToString("F2", CultureInfo.InvariantCulture));
            System.Console.WriteLine("Imposto sobre Conta Poupança: R$ " + conta02.CalcularImposto().ToString("F2", CultureInfo.InvariantCulture));
        }
    }
}
