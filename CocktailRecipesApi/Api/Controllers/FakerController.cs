using Bogus;
using DataAccess;
using Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/faker")]
    [ApiController]
    public class FakerController : ControllerBase
    {
        private readonly CocktailRecipesContext _context;
        private readonly string password = "f1dc735ee3581693489eaf286088b916"; // sifra123

        public FakerController(CocktailRecipesContext context)
        {
            _context = context;
        }

        // POST api/<FakerController>
        [HttpPost]
        public IActionResult Post()
        {
            var typeNames = new List<string> { "alcoholic", "non-alcoholic" };
            var ingredientNames = new List<string> { "Vodka", "Gin", "Rum", "Tequila", "Aftershock", "White Vermouth" };
            var measureNames = new List<string> { "15 ml", "25 ml", "30 ml", "50 ml" };
            var cocktailImages = new List<string> { "https://www.thecocktaildb.com/images/media/drink/metwgh1606770327.jpg", "https://www.thecocktaildb.com/images/media/drink/vrwquq1478252802.jpg", "https://www.thecocktaildb.com/images/media/drink/nkwr4c1606770558.jpg", "https://www.thecocktaildb.com/images/media/drink/qgdu971561574065.jpg" };

            var types = typeNames.Select(t => new Domain.Type { Name = t });
            var ingredients = ingredientNames.Select(i => new Ingredient { Name = i });
            var measures = measureNames.Select(m => new Measure { Name = m });

            _context.Types.AddRange(types);
            _context.Measures.AddRange(measures);
            _context.Ingredients.AddRange(ingredients);

            _context.SaveChanges();

            var typeIds = _context.Types.Select(x => x.Id).ToList();
            var ingredientIds = _context.Ingredients.Select(x => x.Id).ToList();
            var measureIds = _context.Measures.Select(x => x.Id).ToList();

            var admin = new User
            {
                Username = "admin",
                Password = password
            };

            _context.Users.Add(admin);


            var cocktailIngredientFaker = new Faker<CocktailRecipeIngredient>();

            cocktailIngredientFaker.RuleFor(x => x.IngredientId, f => f.PickRandom(ingredientIds));

            var cocktailRecipesFaker = new Faker<CocktailRecipe>();

            cocktailRecipesFaker.RuleFor(x => x.Name, f => f.Commerce.ProductName());
            cocktailRecipesFaker.RuleFor(x => x.Instructions, f => f.Lorem.Text());
            cocktailRecipesFaker.RuleFor(x => x.TypeId, f => f.PickRandom(typeIds));
            cocktailRecipesFaker.RuleFor(x => x.MeasureId, f => f.PickRandom(measureIds));
            cocktailRecipesFaker.RuleFor(x => x.CocktailRecipeIngredients, f => cocktailIngredientFaker.Generate(1));
            cocktailRecipesFaker.RuleFor(x => x.Image, f => f.PickRandom(cocktailImages));

            var cocktailRecipes = cocktailRecipesFaker.Generate(80);
            _context.CocktailRecipes.AddRange(cocktailRecipes);

            _context.SaveChanges();
            return Ok();
        }
    }
}

