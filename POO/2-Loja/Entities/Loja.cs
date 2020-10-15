using System;
using System.Globalization;
using System.Collections.Generic;

namespace _2_Loja.Entities
{
    class Loja
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public List<Livro> Livros { get; set; }
        public List<Videogame> Videogames { get; set; }
        public Loja(){}
        public Loja(string nome, string cnpj, List<Livro> livros, List<Videogame> games)
        {
            Nome = nome;
            Cnpj = cnpj;
            Livros = livros;
            Videogames = games;
        }

        public void ListarLivros()
        {
            if(Livros == null){
                System.Console.WriteLine("A loja americana não tem livros no estoque.");

            }else{
                System.Console.WriteLine("A loja americana possui estes livros para vender:");
                foreach (Livro book in Livros){
                    System.Console.WriteLine($"Titulo: {book.Nome}, preco R$ {book.Preco.ToString("F2", CultureInfo.InvariantCulture)}, quantidade {book.Quantidade} em estoque");
                }
            }
            System.Console.WriteLine("------------------------------------------------");
        }
        public void ListarVideoGames()
        {
            if(Videogames == null){
                System.Console.WriteLine("A loja americana não tem livros no estoque.");
            }else{
                Console.WriteLine("A loja americana possui estes videogames para vender:");
                foreach (Videogame console in Videogames){
                    System.Console.WriteLine($"Videogame: {console.Nome} {console.Modelo}, preco R$ {console.Preco.ToString("F2", CultureInfo.InvariantCulture)}, quantidade {console.Quantidade} em estoque.");
                }
            }
            System.Console.WriteLine("------------------------------------------------");
        }
        public void CalcularPatrimonio()
        {
            double Patrimonio = 0;
            foreach (Livro book in Livros){
                Patrimonio += book.Preco * book.Quantidade;
            }
            foreach (Videogame console in Videogames){
                Patrimonio += console.Preco * console.Quantidade;
                
            }
            System.Console.WriteLine($"O patrimonio da loja é R$ {Patrimonio.ToString("F2", CultureInfo.InvariantCulture)}");
        }
    }
}