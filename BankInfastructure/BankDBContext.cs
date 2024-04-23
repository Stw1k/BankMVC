using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BankDomain.Model;

namespace BankInfastructure;

public partial class BankDBContext : DbContext
{
    public BankDBContext()
    {
    }

    public BankDBContext(DbContextOptions<BankDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<Currency> Currencys { get; set; }

    public virtual DbSet<CustmersOperatino> CustmersOperatinos { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAccoount> CustomerAccoounts { get; set; }

    public virtual DbSet<Operation> Operations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-5JID0EC; Database=ІСТП; Trusted_Connection=True; TrustServerCertificate=True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bank>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BankId).HasColumnName("BankID");

            entity.HasOne(d => d.Bank).WithMany(p => p.Currencies)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK_Currencys_Banks");
        });

        modelBuilder.Entity<CustmersOperatino>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustmersOperatinos)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CustmersOperatinos_Customer");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BankAccountId).HasColumnName("BankAccountID");
            entity.Property(e => e.OperationId).HasColumnName("OperationID");
        });

        modelBuilder.Entity<CustomerAccoount>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerAccoounts)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CustomerAccoounts_Customer");
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.BankId).HasColumnName("BankID");

            entity.HasOne(d => d.Bank).WithMany(p => p.Operations)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK_Operations_Banks");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Operation)
                .HasForeignKey<Operation>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Operations_CustmersOperatinos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
