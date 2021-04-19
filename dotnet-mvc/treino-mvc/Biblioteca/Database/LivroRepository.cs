using System.Collections.Generic;
using System.Linq;
using Biblioteca.Database;
using Biblioteca.Models;

namespace Biblioteca.Database
{
    public class LivroRepository : ILivroRepository
    {
        private readonly ApplicationDbContext Database;
        private readonly SeedingService Seeding;
        public LivroRepository(ApplicationDbContext database, SeedingService seeding)
        {
            this.Database = database;
            Seeding = seeding;
        }


        public List<Livro> getAll()
        {
            List<Livro> livros = Database.Livros.ToList();
            return livros;
        }

        public Livro getById(int id)
        {
            Livro livro = Database.Livros.First(livro => livro.Id.Equals(id));
            return livro;
        }

        public void Add(Livro livro)
        {
            Database.Livros.Add(livro);
            Database.SaveChanges();
        }

        public void RemoveById(int id)
        {
            Livro livro = Database.Livros.First(livro => livro.Id == id);
            Database.Livros.Remove(livro);
            Database.SaveChanges();
        }

        public void RemoveById()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Livro livro)
        {
            Database.Livros.Update(livro);
            Database.SaveChanges();
        }

        public void SeedData()
        {
            if (!Database.Livros.Any())
            {
                Database.AddRange(
                    new Livro(1, "A Lenda dos Guardiões", "Kathryn Lasky", "Fundamento", 230, 12),
                    new Livro(2, "Percy Jackson e o Ladrão de Raios", "Rick Riordan", "Intriseca", 321, 34),
                    new Livro(3, "Clean Code", "Uncle Bob", "Alta Books", 564, 3),
                    new Livro(4, "Rangers A Ordem dos Arqueiros", "John Flanagan", "Fundamento", 300, 10),
                    new Livro(5, "Game Of Thrones", "George R. R. Martin", "Companhia das Letras", 890, 15)
                );
                Database.SaveChanges();
            }
        }
    }
}