using System.Collections.Generic;
using Biblioteca.Models;

namespace Biblioteca.Database
{
    public interface ILivroRepository
    {
        public List<Livro> getAll();
        public Livro getById(int id);
        public void Add(Livro livro);
        public void Update(Livro livro);
        public void RemoveById(int id);
        public void SeedData();
    }
}