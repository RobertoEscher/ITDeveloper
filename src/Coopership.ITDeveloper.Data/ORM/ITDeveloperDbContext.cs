using System.Linq;
using Coopership.ITDeveloper.Data.Mapping;
using Coopership.ITDeveloper.Domain.Entities;
using Coopership.ITDeveloper.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Coopership.ITDeveloper.Data.ORM
{
    public class ITDeveloperDbContext : DbContext
    {
         
        public ITDeveloperDbContext(DbContextOptions<ITDeveloperDbContext> options) : base(options)
        {
        }

        public DbSet<Mural> Mural { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<EstadoPaciente> EstadoPaciente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new EstadoPacienteMap());
            //modelBuilder.ApplyConfiguration(new PacienteMap());


            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
            {
                //property.Relational().ColumnType = "varchar(100)";
                property.SetColumnType("varchar(90)");
            }
                

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ITDeveloperDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}
