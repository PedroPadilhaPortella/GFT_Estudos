using System;

namespace Impares_em_Intervalo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Entre com o valor Inicial e Final: ");
            string[] valores = Console.ReadLine().Split(' ');
            
            int inicial = int.Parse(valores[0]);
            int final = int.Parse(valores[1]);
            System.Console.Write("Saída: ");

            for(int i = inicial; i <= final; i++){
                if(i % 2 != 0){
                    System.Console.Write($"{i} ");
                }
            }
        }
    }
}
