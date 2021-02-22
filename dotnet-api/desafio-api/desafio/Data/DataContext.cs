using desafio.Models;
using Microsoft.EntityFrameworkCore;

namespace desafio.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProdutoVenda> ProdutosVendas { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DataContext(){ }
        public DataContext(DbContextOptions<DataContext> options): base (options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<ProdutoVenda>().HasKey(n => new { n.VendaId, n.ProdutoId } );
        }
    }
}