using System;
using System.Linq;
using System.Collections.Generic;

// namespace _4_Pessoas
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             Pessoa p1 = new Pessoa("João", 15);
//             Pessoa p2 = new Pessoa("Leandro", 21);
//             Pessoa p3 = new Pessoa("Paulo", 17);
//             Pessoa p4 = new Pessoa("Jessica", 18);
//             List<Pessoa> pessoas = new List<Pessoa>();
//             pessoas.Add(p1);
//             pessoas.Add(p2);
//             pessoas.Add(p3);
//             pessoas.Add(p4);

//             //4 Imprimir os Dados e a pessoa mais velha
//             Pessoa older = p1;
//             foreach (var item in pessoas){
//                 if(item.Age > older.Age){
//                     older = item;
//                 }
//             }
//             Console.WriteLine($"Pessoa mais Velha é {older.Name}, que tem {older.Age}");

//             //5 -Excluindo os menores de idade
//             Console.WriteLine("Total de pessoas: " + pessoas.Count);
//             pessoas.RemoveAll(p => p.Age < 18);
//             Console.WriteLine("Todos os menores de idade foram removidos..\nTotal de pessoas: " + pessoas.Count);
//             foreach (Pessoa p in pessoas){
//                 Console.WriteLine(p);
//             }

//             //6- Verificando se a pessoa Jessica está na Lista
//             Pessoa pessoa = p4;
//             bool teste = false;
//             foreach (var item in pessoas){
//                 if(item.Name == pessoa.Name) teste = true;
//             }
//             if(teste)
//                 Console.WriteLine($"{pessoa.Name} está na Lista e sua idade é {pessoa.Age}");
//             else
//                 Console.WriteLine($"{pessoa.Name} não está na Lista!");
//         }
//     }
// }

namespace _4_Pessoas
{
    class Program
    {
        static void Main()
        {
            List<Pessoa> people = new List<Pessoa>()
            {
                new Pessoa("João", 15),
                new Pessoa("Leandro", 21),
                new Pessoa("Paulo", 17),
                new Pessoa("Jessica", 18)
            };
    
            Console.WriteLine("Lista de pessoas:");
            people.ForEach(p => Console.WriteLine(p));
    
            Console.WriteLine();
    
            Pessoa mostOlder = MostOlderPerson(people);
            Console.WriteLine("Pessoa mais velha: " + mostOlder);
    
            Pessoa findJessica = FindPerson("Jessica", people);
            if (findJessica != null)
            {
                Console.WriteLine("Jessica existe e sua idade é: " + findJessica.Age);
            }
    
            RemoveUnder18(people);
    
            Console.WriteLine();
    
            Console.WriteLine("Lista de pessoas atualizada:");
            people.ForEach(p => Console.WriteLine(p));
        }
    
        static Pessoa MostOlderPerson(List<Pessoa> people)
        {
            //Compara duas idades e retorna qual idade é maior.
            return people.Aggregate((p, pp) => p.Age > pp.Age ? p : pp);
        }
    
        static Pessoa FindPerson(string name, List<Pessoa> people)
        {
            return people.Where(p => p.Name.Equals(name)).SingleOrDefault();
        }
    
        static void RemoveUnder18(List<Pessoa> people)
        {
            people.RemoveAll(p => p.Age < 18);
        }
    }
}