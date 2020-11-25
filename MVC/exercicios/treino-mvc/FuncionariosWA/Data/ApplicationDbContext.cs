using System;
using System.Collections.Generic;
using System.Text;
using FuncionariosWA.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FuncionariosWA.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<LocalDeTrabalho> LocaisDeTrabalho { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Tecnologia> Tecnologias { get; set; }
        public DbSet<Vaga> Vagas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Alocacao> Alocacao { get; set; }
        public DbSet<VagaTecnologia> VagaTecnologias { get; set; }
        public DbSet<FuncionarioTecnologia> FuncionarioTecnologias { get; set; }

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<VagaTecnologia>().HasKey(vt => new { vt.VagaId, vt.TecnologiaId } );
            modelBuilder.Entity<FuncionarioTecnologia>().HasKey(ft => new { ft.FuncionarioId, ft.TecnologiaId } );
        }
    }
}