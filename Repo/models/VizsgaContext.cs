using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Repo.models;

public partial class VizsgaContext : DbContext
{
    public VizsgaContext()
    {
    }

    public VizsgaContext(DbContextOptions<VizsgaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Orarend> Orarends { get; set; }

    public virtual DbSet<Szint> Szints { get; set; }

    public virtual DbSet<Tanar> Tanars { get; set; }

    public virtual DbSet<Tanc> Tancs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=127.0.0.1;database=vizsga;uid=root;pwd=jelszo", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.3.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Orarend>(entity =>
        {
            entity.HasKey(e => e.OrarendId).HasName("PRIMARY");

            entity
                .ToTable("orarend")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_hungarian_ci");

            entity.HasIndex(e => e.Szint, "szint");

            entity.HasIndex(e => e.Tanar1, "tanar1");

            entity.HasIndex(e => e.Tanar2, "tanar2");

            entity.HasIndex(e => e.Tanc, "tanc");

            entity.Property(e => e.OrarendId).HasColumnName("orarend_id");
            entity.Property(e => e.Hossz).HasColumnName("hossz");
            entity.Property(e => e.KezdoIdopont)
                .HasMaxLength(7)
                .HasColumnName("kezdo_idopont")
                .UseCollation("utf8mb3_general_ci");
            entity.Property(e => e.Letszam).HasColumnName("letszam");
            entity.Property(e => e.Nap)
                .HasMaxLength(13)
                .HasColumnName("nap")
                .UseCollation("utf8mb3_general_ci");
            entity.Property(e => e.Szint).HasColumnName("szint");
            entity.Property(e => e.Tanar1).HasColumnName("tanar1");
            entity.Property(e => e.Tanar2).HasColumnName("tanar2");
            entity.Property(e => e.Tanc).HasColumnName("tanc");
            entity.Property(e => e.Terem).HasColumnName("terem");

            entity.HasOne(d => d.SzintNavigation).WithMany(p => p.Orarends)
                .HasForeignKey(d => d.Szint)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orarend_ibfk_1");

            entity.HasOne(d => d.Tanar1Navigation).WithMany(p => p.OrarendTanar1Navigations)
                .HasForeignKey(d => d.Tanar1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orarend_ibfk_3");

            entity.HasOne(d => d.Tanar2Navigation).WithMany(p => p.OrarendTanar2Navigations)
                .HasForeignKey(d => d.Tanar2)
                .HasConstraintName("orarend_ibfk_4");

            entity.HasOne(d => d.TancNavigation).WithMany(p => p.Orarends)
                .HasForeignKey(d => d.Tanc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orarend_ibfk_2");
        });

        modelBuilder.Entity<Szint>(entity =>
        {
            entity.HasKey(e => e.SzintId).HasName("PRIMARY");

            entity
                .ToTable("szint")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_hungarian_ci");

            entity.Property(e => e.SzintId).HasColumnName("szint_id");
            entity.Property(e => e.Ar).HasColumnName("ar");
            entity.Property(e => e.Kategoria)
                .HasMaxLength(15)
                .HasColumnName("kategoria")
                .UseCollation("utf8mb3_general_ci");
        });

        modelBuilder.Entity<Tanar>(entity =>
        {
            entity.HasKey(e => e.TanarId).HasName("PRIMARY");

            entity
                .ToTable("tanar")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_hungarian_ci");

            entity.Property(e => e.TanarId).HasColumnName("tanar_id");
            entity.Property(e => e.Email)
                .HasMaxLength(37)
                .HasColumnName("email")
                .UseCollation("utf8mb3_general_ci");
            entity.Property(e => e.Nev)
                .HasMaxLength(25)
                .HasColumnName("nev")
                .UseCollation("utf8mb3_general_ci");
        });

        modelBuilder.Entity<Tanc>(entity =>
        {
            entity.HasKey(e => e.TancId).HasName("PRIMARY");

            entity
                .ToTable("tanc")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_hungarian_ci");

            entity.Property(e => e.TancId).HasColumnName("tanc_id");
            entity.Property(e => e.TancTipus)
                .HasMaxLength(24)
                .HasColumnName("tanc_tipus")
                .UseCollation("utf8mb3_general_ci");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
