using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DataAccess.Configurations
{
    public class CocktailRecipeConfiguration : IEntityTypeConfiguration<CocktailRecipe>
    {
        public void Configure(EntityTypeBuilder<CocktailRecipe> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired();
            builder.Property(c => c.Image)
                .IsRequired();
            builder.Property(c => c.Instructions)
                .IsRequired();

            builder.HasMany(c => c.CocktailRecipeIngredients)
                .WithOne(ci => ci.CocktailRecipe)
                .HasForeignKey(ci => ci.CocktailRecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
