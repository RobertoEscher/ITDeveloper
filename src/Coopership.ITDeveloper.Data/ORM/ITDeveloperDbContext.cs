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
    }
}
