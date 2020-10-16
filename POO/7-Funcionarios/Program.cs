using System;
using _7_Funcionarios.Entities;

namespace _7_Funcionarios
{
    class Program
    {
        static void Main(string[] args)
        {
            Funcionario f1 = new Funcionario("Pedro", 19, 1600);
            Funcionario f2 = new Gerente("Clécio", 30, 5000);
            Funcionario f3 = new Supervisor("Edwin", 21, 2200);
            Funcionario f4 = new Vendedor("Fernando", 29, 12000);

            Console.WriteLine($"Salario do {f1.Nome}: R${f1.Bonificacao()}");
            Console.WriteLine($"Salario do {f2.Nome}: R${f2.Bonificacao()}");
            Console.WriteLine($"Salario do {f3.Nome}: R${f3.Bonificacao()}");
            Console.WriteLine($"Salario do {f4.Nome}: R${f4.Bonificacao()}");
        }
    }
}
