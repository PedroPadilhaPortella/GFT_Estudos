using System.Linq;
using Biblioteca.Models;

namespace Biblioteca.Database
{
    public class SeedingService
    {
        private ApplicationDbContext Database; //injeção de dependencia do banco de dados
        public SeedingService(ApplicationDbContext database){
            this.Database = database;
        }

         public void Seed()
        {
            if(Database.Livros.Any()){
                return; //DB has been populated!
            }
            Database.AddRange(
                new Livro(1, "A Lenda dos Guardiões", "Kathryn Lasky", 230, 12),
                new Livro(2, "Percy Jackson e o Ladrão de Raios", "Rick Riordan", 321, 34),
                new Livro(3, "Clean Code", "Uncle Bob", 564, 3),
                new Livro(4, "Rangers A Ordem dos Arqueiros", "John Flanagan", 300, 10)
            );
            Database.SaveChanges();
        }
    }
}