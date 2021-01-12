using Microsoft.EntityFrameworkCore;
using NotaFiscal.Models;

namespace NotaFiscal.Data
{
    public class NotaFiscalContext: DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<NotaFiscal.Models.NotaFiscal> NotasFiscais { get; set; }
        public DbSet<ProdutoNotaFiscal> ProdutosNotaFiscal { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public NotaFiscalContext() { }
        public NotaFiscalContext(DbContextOptions<NotaFiscalContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<ProdutoNotaFiscal>().HasKey(n => new { n.NotaFiscalId, n.ProdutoId } );
            modelBuilder.Entity<ProdutoNotaFiscal>().HasOne(n => n.NotaFiscal).WithMany(n => n.ProdutosNotaFiscal).HasForeignKey(n => n.NotaFiscalId);  
            modelBuilder.Entity<ProdutoNotaFiscal>().HasOne(n => n.Produto).WithMany(n => n.ProdutosNotaFiscal).HasForeignKey(n => n.ProdutoId);
        }
    }
}