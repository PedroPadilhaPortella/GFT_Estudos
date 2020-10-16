using System;
using System.Collections.Generic;

namespace _4_Pessoas
{
    class Program
    {
        static void Main(string[] args)
        {
            Pessoa p1 = new Pessoa("João", 15);
            Pessoa p2 = new Pessoa("Leandro", 21);
            Pessoa p3 = new Pessoa("Paulo", 17);
            Pessoa p4 = new Pessoa("Jessica", 18);
            List<Pessoa> pessoas = new List<Pessoa>();
            pessoas.Add(p1);
            pessoas.Add(p2);
            pessoas.Add(p3);
            pessoas.Add(p4);

            //4 Imprimir os Dados e a pessoa mais velha
            Pessoa older = p1;
            foreach (var item in pessoas){
                if(item.Age > older.Age){
                    older = item;
                }
            }
            Console.WriteLine($"Pessoa mais Velha é {older.Name}, que tem {older.Age}");

            //5 -Excluindo os menores de idade
            Console.WriteLine("Total de pessoas: " + pessoas.Count);
            pessoas.RemoveAll(p => p.Age < 18);
            Console.WriteLine("Todos os menores de idade foram removidos..\nTotal de pessoas: " + pessoas.Count);
            foreach (Pessoa p in pessoas){
                Console.WriteLine(p);
            }

            //6- Verificando se a pessoa Jessica está na Lista
            Pessoa pessoa = p4;
            bool teste = false;
            foreach (var item in pessoas){
                if(item.Name == pessoa.Name) teste = true;
            }
            if(teste)
                Console.WriteLine($"{pessoa.Name} está na Lista e sua idade é {pessoa.Age}");
            else
                Console.WriteLine($"{pessoa.Name} não está na Lista!");
        }
    }
}