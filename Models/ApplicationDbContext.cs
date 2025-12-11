using API_de_Clientes__sin_autenticación_.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Clientes.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           

            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Email)
                .IsUnique();
          

            modelBuilder.Entity<Cliente>()
                .Property(c => c.FechaDeRegistro)
                .HasDefaultValueSql("GETDATE()");
           
        }
    }
}
