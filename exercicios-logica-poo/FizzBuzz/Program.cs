using System;
using System.Collections.Generic;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            // int n = int.Parse(Console.ReadLine());

            string[] data = Console.ReadLine().Split();

            List<int> lista = new List<int>();

            for (int i = 0; i < data.Length; i++)
            {
                lista.Add(int.Parse(data[i]));
            }

            foreach (int numero in lista)
            {
                for (int i = 1; i <= numero; i++)
                {
                    if (i % 3 == 0 && i % 5 == 0) {
                        System.Console.WriteLine("FizzBuzz");
                    } else if(i % 3 == 0) {
                        System.Console.WriteLine("Fizz");
                    } else if(i % 5 == 0) {
                        System.Console.WriteLine("Buzz");
                    } else {
                        System.Console.WriteLine(i);
                    }
                }
            }
        }
    }
}
