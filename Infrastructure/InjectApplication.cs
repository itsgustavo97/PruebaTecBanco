using Application.Contracts.Infrastructure;
using Application.Models.Security;
using Domain;
using Infrastructure.Persistencia;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class InjectApplication
    {
        public static IServiceCollection InjectService(this IServiceCollection services, IConfiguration config)
        {
            //Primera conexion
            services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(config.GetConnectionString("default"),
            sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName);
                sqlOptions.EnableRetryOnFailure();

            }));

            services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["JwtSettings:Key"]))
            };

            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped(typeof(IRepositoryGeneric<>), typeof(RepositoryGeneric<>));
            services.AddTransient<IReportService, ReportService>();
            services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
            return services;
        }
    }
}
