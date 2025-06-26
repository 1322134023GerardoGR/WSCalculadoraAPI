using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WSCalculadoraAPI.Models;

namespace WSCalculadoraAPI.Data
{
    public class CalculadoraContext : DbContext
    {
        public CalculadoraContext(DbContextOptions<CalculadoraContext> options) : base(options)
        {
        }

        public DbSet<Aritmetica> Calculadora { get; set; } 
    }
}