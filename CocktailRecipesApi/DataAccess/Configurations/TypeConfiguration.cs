using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class TypeConfiguration : IEntityTypeConfiguration<Domain.Type>
    {
        public void Configure(EntityTypeBuilder<Domain.Type> builder)
        {
            builder.Property(t => t.Name)
                .IsRequired();

            builder.HasMany(t => t.CocktailRecipes)
                .WithOne(c => c.Type)
                .HasForeignKey(c => c.TypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
