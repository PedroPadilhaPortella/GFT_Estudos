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
        
        // public DbSet<VagaTecnologia> VagaTecnologias { get; set; }
        // public DbSet<FuncionarioTecnologia> FuncionarioTecnologias { get; set; }

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // public void OnModelCreatingPartial(ModelBuilder modelBuilder) {}
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Cargo>(entity =>
        //     {
        //         entity.ToTable("cargo");

        //         entity.Property(e => e.Id).HasColumnType("int(11)");

        //         entity.Property(e => e.Nome)
        //             .IsRequired()
        //             .HasColumnType("varchar(100)")
        //             .HasCharSet("utf8")
        //             .HasCollation("utf8_general_ci");
        //     });

        //     modelBuilder.Entity<Funcionario>(entity =>
        //     {
        //         entity.ToTable("funcionario");

        //         entity.HasIndex(e => e.CargoId)
        //             .HasName("Funcionario3_idx");

        //         entity.HasIndex(e => e.LocalDeTrabalhoId)
        //             .HasName("Funcionario1_idx");

        //         entity.HasIndex(e => e.VagaId)
        //             .HasName("Funcionario2_idx");

        //         entity.Property(e => e.Id).HasColumnType("int(11)");

        //         entity.Property(e => e.CargoId).HasColumnType("int(11)");

        //         entity.Property(e => e.InicioWa)
        //             .HasColumnName("Inicio_wa")
        //             .HasColumnType("date");

        //         entity.Property(e => e.LocalDeTrabalhoId).HasColumnType("int(11)");

        //         entity.Property(e => e.Matricula)
        //             .IsRequired()
        //             .HasColumnType("varchar(20)")
        //             .HasCharSet("utf8")
        //             .HasCollation("utf8_general_ci");

        //         entity.Property(e => e.Nome)
        //             .IsRequired()
        //             .HasColumnType("varchar(100)")
        //             .HasCharSet("utf8")
        //             .HasCollation("utf8_general_ci");

        //         entity.Property(e => e.TerminoWa)
        //             .HasColumnName("Termino_wa")
        //             .HasColumnType("date");

        //         entity.Property(e => e.VagaId).HasColumnType("int(11)");

        //         entity.HasOne(d => d.Cargo)
        //             .WithMany(p => p.Funcionario)
        //             .HasForeignKey(d => d.CargoId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("Funcionario3");

        //         entity.HasOne(d => d.LocalDeTrabalho)
        //             .WithMany(p => p.Funcionario)
        //             .HasForeignKey(d => d.LocalDeTrabalhoId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("Funcionario1");

        //         entity.HasOne(d => d.Vaga)
        //             .WithMany(p => p.Funcionario)
        //             .HasForeignKey(d => d.VagaId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("Funcionario2");
        //     });

        //     modelBuilder.Entity<FuncionarioTecnologia>(entity =>
        //     {
        //         entity.HasNoKey();

        //         entity.ToTable("funcionariotecnologia");

        //         entity.HasIndex(e => e.FuncionarioId)
        //             .HasName("FuncionarioTecnologia1_idx");

        //         entity.HasIndex(e => e.TecnologiaId)
        //             .HasName("FuncionarioTecnologia2_idx");

        //         entity.Property(e => e.FuncionarioId).HasColumnType("int(11)");

        //         entity.Property(e => e.TecnologiaId).HasColumnType("int(11)");

        //         entity.HasOne(d => d.Funcionario)
        //             .WithMany()
        //             .HasForeignKey(d => d.FuncionarioId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("FuncionarioTecnologia1");

        //         entity.HasOne(d => d.Tecnologia)
        //             .WithMany()
        //             .HasForeignKey(d => d.TecnologiaId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("FuncionarioTecnologia2");
        //     });

        //     modelBuilder.Entity<LocalDeTrabalho>(entity =>
        //     {
        //         entity.ToTable("localdetrabalho");

        //         entity.Property(e => e.Id).HasColumnType("int(11)");

        //         entity.Property(e => e.Cep)
        //             .IsRequired()
        //             .HasColumnType("varchar(15)")
        //             .HasCharSet("utf8")
        //             .HasCollation("utf8_general_ci");

        //         entity.Property(e => e.Cidade)
        //             .IsRequired()
        //             .HasColumnType("varchar(100)")
        //             .HasCharSet("utf8")
        //             .HasCollation("utf8_general_ci");

        //         entity.Property(e => e.Endereco)
        //             .IsRequired()
        //             .HasColumnType("varchar(200)")
        //             .HasCharSet("utf8")
        //             .HasCollation("utf8_general_ci");

        //         entity.Property(e => e.Estado)
        //             .IsRequired()
        //             .HasColumnType("varchar(50)")
        //             .HasCharSet("utf8")
        //             .HasCollation("utf8_general_ci");

        //         entity.Property(e => e.Nome)
        //             .IsRequired()
        //             .HasColumnType("varchar(45)")
        //             .HasCharSet("utf8")
        //             .HasCollation("utf8_general_ci");

        //         entity.Property(e => e.Telefone)
        //             .IsRequired()
        //             .HasColumnType("varchar(30)")
        //             .HasCharSet("utf8")
        //             .HasCollation("utf8_general_ci");
        //     });

        //     modelBuilder.Entity<Tecnologia>(entity =>
        //     {
        //         entity.ToTable("tecnologia");

        //         entity.Property(e => e.Id).HasColumnType("int(11)");

        //         entity.Property(e => e.Nome)
        //             .IsRequired()
        //             .HasColumnType("varchar(50)")
        //             .HasCharSet("utf8")
        //             .HasCollation("utf8_general_ci");
        //     });

        //     modelBuilder.Entity<Vaga>(entity =>
        //     {
        //         entity.ToTable("vaga");

        //         entity.HasIndex(e => e.CargoId)
        //             .HasName("Vaga1_idx");

        //         entity.Property(e => e.Id).HasColumnType("int(11)");

        //         entity.Property(e => e.AberturaDaVaga).HasColumnType("date");

        //         entity.Property(e => e.CargoId).HasColumnType("int(11)");

        //         entity.Property(e => e.CodigoDaVaga)
        //             .IsRequired()
        //             .HasColumnType("varchar(30)")
        //             .HasCharSet("utf8")
        //             .HasCollation("utf8_general_ci");

        //         entity.Property(e => e.Descricao)
        //             .IsRequired()
        //             .HasColumnType("varchar(100)")
        //             .HasCharSet("utf8")
        //             .HasCollation("utf8_general_ci");

        //         entity.Property(e => e.Projeto)
        //             .IsRequired()
        //             .HasColumnType("varchar(50)")
        //             .HasCharSet("utf8")
        //             .HasCollation("utf8_general_ci");

        //         entity.Property(e => e.QuantidadeDeVagas).HasColumnType("int(11)");

        //         entity.HasOne(d => d.Cargo)
        //             .WithMany(p => p.Vaga)
        //             .HasForeignKey(d => d.CargoId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("Vaga1");
        //     });

        //     modelBuilder.Entity<VagaTecnologia>(entity =>
        //     {
        //         entity.HasNoKey();

        //         entity.ToTable("vagatecnologia");

        //         entity.HasIndex(e => e.TecnologiaId)
        //             .HasName("VagaTecnologia2_idx");

        //         entity.HasIndex(e => e.VagaId)
        //             .HasName("VagaTecnologia1_idx");

        //         entity.Property(e => e.TecnologiaId).HasColumnType("int(11)");

        //         entity.Property(e => e.VagaId).HasColumnType("int(11)");

        //         entity.HasOne(d => d.Tecnologia)
        //             .WithMany()
        //             .HasForeignKey(d => d.TecnologiaId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("VagaTecnologia2");

        //         entity.HasOne(d => d.Vaga)
        //             .WithMany()
        //             .HasForeignKey(d => d.VagaId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("VagaTecnologia1");
        //     });

        //     OnModelCreatingPartial(modelBuilder);
        // }
    }
}