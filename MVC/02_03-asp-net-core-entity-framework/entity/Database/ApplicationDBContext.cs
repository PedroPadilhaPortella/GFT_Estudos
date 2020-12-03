using Microsoft.EntityFrameworkCore;
using entity.Models;


namespace entity.Database
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) :base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            //Configurando o LazyLoading, para não precisar usar Include nas consultar intertabelas do asp.net
            optionsBuilder.UseLazyLoadingProxies();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            //FluentAPI, que habilita a edição e customização das tabelas
            modelBuilder.Entity<Produto>().ToTable("products"); //altera o nome da tabela
            // modelBuilder.Entity<Produto>().Property(p => p.Nome).IsRequired(); //torna required o campo Nome
            modelBuilder.Entity<Produto>().Property(p => p.Nome).HasMaxLength(100); //maximo de caracteres
            // modelBuilder.Entity<Produto>().HasKey(p => p.Codigo);//define este campo como chave primária
        }

    }
}