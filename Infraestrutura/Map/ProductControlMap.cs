using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ValidityControl.DoMain.Models;

namespace ValidityControl.Infraestrutura.Map
{
    public class ProductControlMap : IEntityTypeConfiguration<ProductControl>
    {
        public void Configure(EntityTypeBuilder<ProductControl> builder)
        {
            builder.HasKey(x => x.ean);
            builder.Property(x => x.Validity).IsRequired();
<<<<<<< HEAD
                    }
=======
        }
>>>>>>> a3f0c3a (Atualização do projeto ValidityControl)
    }
}

