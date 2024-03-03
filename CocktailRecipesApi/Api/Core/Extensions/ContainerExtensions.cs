using Api.Core.FakeActors;
using Api.Core.Jwt;
using Application.Commands.IngredientCommands;
using Application.Commands.RecipeCommands;
using Application.Queries.CocktailQueries;
using Application.Queries.IngredientQueries;
using Application.Queries.MeasureQueries;
using Application.Queries.RecipeQueries;
using Application.Queries.TypeQueries;
using Application;
using Implementation.Commands.IngredientCommands;
using Implementation.Commands.RecipeCommands;
using Implementation.Queries.CocktailQueries;
using Implementation.Queries.IngredientQueries;
using Implementation.Queries.MeasureQueries;
using Implementation.Queries.RecipeQueries;
using Implementation.Queries.TypeQueries;
using Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace Api.Core.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IGetTypesQuery, EFGetTypesQuery>();

            services.AddTransient<IGetMeasuresQuery, EFGetMeasuresQuery>();

            services.AddTransient<IGetCocktailsQuery, EFGetCocktailsQuery>();
            services.AddTransient<IGetCocktailQuery, EFGetCocktailQuery>();

            services.AddTransient<IGetRecipesQuery, EFGetRecipesQuery>();
            services.AddTransient<IGetRecipeQuery, EFGetRecipeQuery>();
            services.AddTransient<ICreateRecipeCommand, EFCreateRecipeCommand>();
            services.AddTransient<IUpdateRecipeCommand, EFUpdateRecipeCommand>();
            services.AddTransient<IDeleteRecipeCommand, EFDeleteRecipeCommand>();

            services.AddTransient<IGetIngredientQuery, EFGetIngredientQuery>();
            services.AddTransient<IGetIngredientsQuery, EFGetIngredientsQuery>();
            services.AddTransient<ICreateIngredientCommand, EFCreateIngredientCommand>();
            services.AddTransient<IUpdateIngredientCommand, EFUpdateIngredientCommand>();
            services.AddTransient<IDeleteIngredientCommand, EFDeleteIngredientCommand>();

            services.AddTransient<UseCaseExecutor>();
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<CreateRecipeValidator>();
            services.AddTransient<UpdateRecipeValidator>();
            services.AddTransient<CreateIngredientValidator>();
            services.AddTransient<UpdateIngredientValidator>();
            services.AddTransient<UserLoginRequestValidator>();
        }

        public static void AddApplicationActor(this IServiceCollection services)
        {
            services.AddTransient<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();

                var user = accessor.HttpContext.User;

                if (user.FindFirst("ActorData") == null)
                {
                    return new UnauthorizedActor();
                }

                var actorString = user.FindFirst("ActorData").Value;

                var actor = JsonConvert.DeserializeObject<JwtActor>(actorString);

                return actor;

            });
        }

        public static void AddJwt(this IServiceCollection services)
        {
            services.AddTransient<JwtManager>();

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "asp_api",
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMyVerySecretKey")),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
