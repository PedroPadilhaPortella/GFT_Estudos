using System;
using System.Globalization;
using System.Collections.Generic;
using _2_Loja.Entities;

namespace _2_Loja
{
    class Program
    {
        static void Main(string[] args)
        {
            Livro l1 = new Livro("Harry Potter", 40.0, 50, "J K Rowling", "fantasia", 300);
            Livro l2 = new Livro("Senhor dos Anéis", 60.0, 30, "J R R Tolkien", "fantasia", 500);
            Livro l3 =  new Livro("Java POO", 20.0, 50, "GFT", "educativo", 500);
            
            Videogame ps4 = new Videogame("PS4", 1000, 100, "Sony", "Slim", false);
            Videogame ps4Usado = new Videogame("PS4", 1000, 7, "Sony", "Slim", true);
            Videogame xbox = new Videogame("XBOX", 1500, 500, "Microsoft", "One", false);

            List<Livro> livros = new List<Livro>();
            livros.Add(l1);
            livros.Add(l2);
            livros.Add(l3);

            List<Videogame> games = new List<Videogame>();
            games.Add(ps4);
            games.Add(ps4Usado);
            games.Add(xbox);

            Loja americanas = new Loja("Americanas", "12345678", livros, games);

            l2.CalcularImposto();
            l3.CalcularImposto();
            ps4Usado.CalcularImposto();
            ps4.CalcularImposto();

            americanas.ListarLivros();
            americanas.ListarVideoGames();
            americanas.CalcularPatrimonio();
        }
    }
}
