using System;

namespace _03_Conta
{
    class Program
    {
        static void Main(string[] args)
        {
            ContaCorrente conta01 = new ContaCorrente("Pedro Portella", "Pedro", 1000);
            ContaPoupanca conta02 = new ContaPoupanca("Pedro Portella", "Pedro", 1000);

            System.Console.WriteLine("Rendimento Conta Corrente: " + conta01.Rendimento());
            System.Console.WriteLine("Rendimento Conta Poupanca: " + conta02.Rendimento());
            System.Console.WriteLine(conta01);
            System.Console.WriteLine(conta02);
        }
    }
}
