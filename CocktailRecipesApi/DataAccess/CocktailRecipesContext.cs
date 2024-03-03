using DataAccess.Configurations;
using Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CocktailRecipesContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TypeConfiguration());
            modelBuilder.ApplyConfiguration(new IngredientConfiguration());
            modelBuilder.ApplyConfiguration(new CocktailRecipeConfiguration());
            modelBuilder.ApplyConfiguration(new MeasureConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<CocktailRecipeIngredient>().HasKey(x => new { x.CocktailRecipeId, x.IngredientId });

            modelBuilder.Entity<Domain.Type>().HasQueryFilter(x => !x.DeleteDate.HasValue);
            modelBuilder.Entity<CocktailRecipe>().HasQueryFilter(x => !x.DeleteDate.HasValue);
            modelBuilder.Entity<Ingredient>().HasQueryFilter(x => !x.DeleteDate.HasValue);
            modelBuilder.Entity<Measure>().HasQueryFilter(x => !x.DeleteDate.HasValue);
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.DeleteDate.HasValue);

        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.CreationDate = DateTime.UtcNow;
                            e.UpdateDate = null;
                            e.DeleteDate = null;
                            break;
                        case EntityState.Modified:
                            e.UpdateDate = DateTime.UtcNow;
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=CocktailRecipesDatabase;Integrated Security=True");
        }

        public DbSet<Domain.Type> Types { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<CocktailRecipe> CocktailRecipes { get; set; }
        public DbSet<CocktailRecipeIngredient> CocktailRecipeIngredients { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
