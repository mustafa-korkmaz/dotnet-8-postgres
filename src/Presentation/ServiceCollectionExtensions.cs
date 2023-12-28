using Application.Constants;
using Application.Services.Account;
using Application.Services.Order;
using Application.Services.Product;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Presentation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigSections(
           this IServiceCollection services, IConfiguration config)
        {
            //services.Configure<SomeConfig>(
            //    config.GetSection("SomeConfig"));

            return services;
        }

        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            //ViewModels to DTOs mappings
            services.AddAutoMapper(typeof(PresentationMappingProfile));

            //DTOs to Domain entities mappings
            services.AddAutoMapper(typeof(Application.ApplicationMappingProfile));

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IAccountService, AccountService>();

            return services;
        }

        public static void ConfigureJwtAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(AppConstants.DefaultAuthorizationPolicy, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }

        public static void ConfigureJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = JwtTokenConstants.IssuerSigningKey,
                        ValidAudience = JwtTokenConstants.Audience,
                        ValidIssuer = JwtTokenConstants.Issuer,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(0)
                    };
                });
        }
    }
}


