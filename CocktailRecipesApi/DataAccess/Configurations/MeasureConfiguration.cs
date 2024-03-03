using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class MeasureConfiguration : IEntityTypeConfiguration<Measure>
    {
        public void Configure(EntityTypeBuilder<Measure> builder)
        {
            builder.Property(m => m.Name)
                .IsRequired();

            builder.HasMany(m => m.CocktailRecipes)
                .WithOne(c => c.Measure)
                .HasForeignKey(c => c.MeasureId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}