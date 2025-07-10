using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ValidityControl.DoMain.Models;

namespace ValidityControl.Infraestrutura.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuarioModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.name).IsRequired();
            builder.Property(x => x.password).IsRequired();
        }
    }
}
