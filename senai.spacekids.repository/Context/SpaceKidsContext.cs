using Microsoft.EntityFrameworkCore;
using  senai.spacekids.domain.Entities;
namespace senai.spacekids.repository.Context
{
    public class SpaceKidsContext:DbContext
    {
        public SpaceKidsContext(DbContextOptions<SpaceKidsContext> options): base(options){}

        public DbSet<Login> Logins{get;set;}
        public DbSet<Pai> Pais{get;set;}
        public DbSet<Crianca> Criancas{get;set;}
        public DbSet<Fase> Fases{get;set;} 
        public DbSet<Desempenho> Desempenhos{get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>().ToTable("Logins");
            modelBuilder.Entity<Pai>().ToTable("Pais");
            modelBuilder.Entity<Crianca>().ToTable("Criancas");
            modelBuilder.Entity<Fase>().ToTable("Fases");
            modelBuilder.Entity<Desempenho>().ToTable("Desempenhos");

            modelBuilder.Entity<Login>().HasIndex(c => c.email).IsUnique();
            base.OnModelCreating(modelBuilder);


        }
        
    }
}