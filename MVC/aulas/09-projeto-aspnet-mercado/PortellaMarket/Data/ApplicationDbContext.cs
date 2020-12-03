using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortellaMarket.Models;

namespace PortellaMarket.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //mapeamento de entidades
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Saida> Saidas { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
