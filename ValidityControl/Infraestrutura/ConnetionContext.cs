using Microsoft.EntityFrameworkCore;
using ValidityControl.DoMain.Models;
using ValidityControl.Infraestrutura.Map;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ValidityControl.Infraestrutura
{
    public class ConnetionContext : DbContext
    {
        public DbSet<UsuarioModel> usuarios { get; set; }
        public DbSet<ProductControl> productControls { get; set; }
     
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder )

        => optionsBuilder.UseNpgsql(
            "Host=localhost;" +
            "Port=5432;  DataBase=postgres;" +
            "User Id=postgres;" +
            "Password=ishmael;" +
            "Include Error Detail = true"
            );

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new  ProductControlMap());
            base.OnModelCreating(modelBuilder);
        }


    }
}
