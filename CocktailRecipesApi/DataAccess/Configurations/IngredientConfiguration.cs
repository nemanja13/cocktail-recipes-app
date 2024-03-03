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
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.Property(i => i.Name)
                .IsRequired();

            builder.HasMany(i => i.CocktailRecipeIngredients)
                .WithOne(ci => ci.Ingredient)
                .HasForeignKey(ci => ci.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
