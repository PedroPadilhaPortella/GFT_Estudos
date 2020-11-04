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
        public DbSet<GFT> GFT { get; set; }
        public DbSet<Tecnologia> Tecnologias { get; set; }
        public DbSet<Vaga> Vagas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<VagaTecnologia> VagaTecnologias { get; set; }
        public DbSet<FuncionarioTecnologia> FuncionarioTecnologias { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<VagaTecnologia>()
        //         .HasKey(vt => new { vt.VagaId, vt.TecnologiaId });

        //     modelBuilder.Entity<VagaTecnologia>()
        //         .HasOne(vt => vt.Vaga)
        //         .WithMany(v => v.VagaTecnologias)
        //         .HasForeignKey(vt => vt.VagaId);

        //     modelBuilder.Entity<VagaTecnologia>()
        //         .HasOne(vt => vt.Tecnologia)
        //         .WithMany(t => t.VagaTecnologias)
        //         .HasForeignKey(vt => vt.TecnologiaId);


        //     modelBuilder.Entity<FuncionarioTecnologia>()
        //         .HasKey(ft => new { ft.FuncionarioId, ft.TecnologiaId });

        //     modelBuilder.Entity<FuncionarioTecnologia>()
        //         .HasOne(ft => ft.Funcionario)
        //         .WithMany(f => f.FuncionarioTecnologias)
        //         .HasForeignKey(ft => ft.FuncionarioId);

        //     modelBuilder.Entity<FuncionarioTecnologia>()
        //         .HasOne(ft => ft.Tecnologia)
        //         .WithMany(t => t.FuncionarioTecnologias)
        //         .HasForeignKey(ft => ft.TecnologiaId);
        // }
    }
}