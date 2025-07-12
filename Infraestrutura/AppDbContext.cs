using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using ValidityControl.DoMain.Models;
using ValidityControl.Infraestrutura.Map;


namespace ValidityControl.Infraestrutura
{
    public class AppDbContext : DbContext
    {
        public DbSet<UsuarioModel> usuarios { get; set; }
        public DbSet<ProductControl> productControls { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new ProductControlMap());
            base.OnModelCreating(modelBuilder);
        }


    }
}
