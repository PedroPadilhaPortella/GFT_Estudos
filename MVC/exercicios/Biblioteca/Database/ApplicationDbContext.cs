using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) { }
    }
}