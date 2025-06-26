using Microsoft.EntityFrameworkCore;
using WSCalculadoraAPI.Models;

namespace WSCalculadoraAPI.Data
{
    public class CalculadoraContext : DbContext
    {
        public CalculadoraContext(DbContextOptions<CalculadoraContext> options) : base(options)
        {
        }

        public DbSet<Aritmetica> Calculadora { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aritmetica>(entity =>
            {
                entity.ToTable("Calculadora");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Num1).HasColumnName("num1");
                entity.Property(e => e.Num2).HasColumnName("num2");
                entity.Property(e => e.Operation).HasColumnName("operation");
                entity.Property(e => e.Result).HasColumnName("result");
                entity.Property(e => e.CreatedAt).HasColumnName("createdat");
            });
        }
    }
}