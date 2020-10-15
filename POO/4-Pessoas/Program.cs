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
            Console.WriteLine("Total de Pessoas: " + Pessoa.totalPessoas);

            //4 Imprimir os Dados e a pessoa mais velha
            int maiorAge = 0;
            string maiorName = "";

            for (int i = 0; i < pessoas.Count; i++)
            {
                System.Console.WriteLine(pessoas[i]);
                if(pessoas[i] == Pessoa.Parse(0)){
                    maiorAge = pessoas[i].Age;
                    maiorName = pessoas[i].Name;
                }else if(pessoas[i].Age > maiorAge){
                    maiorAge = pessoas[i].Age;
                    maiorName = pessoas[i].Name;
                }
            }
            System.Console.WriteLine($"A pessoa mais velha é o {maiorName}, com {maiorAge} anos");
            // pessoas.RemoveAll(p => p.Age < 18);
            // Console.WriteLine("Total de Pessoas: " + Pessoa.totalPessoas);


            // //Excluindo os menores
            // foreach (Pessoa p in pessoas){
            //     Console.WriteLine(p);
            // }
        }
    }
}