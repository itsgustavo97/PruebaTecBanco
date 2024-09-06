using Application.Contracts.Security;
using Application.Models.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Security.Identity;
using Security.Persistencia;
using Security.Servicios;
using System.Text;

namespace Security
{
    public static class InjectApplication 
    {
        public static IServiceCollection InjectServiceSecurity(this IServiceCollection services, IConfiguration IConfig)
        {
            services.Configure<JwtSettings>(IConfig.GetSection("JwtSettings"));
            services.AddDbContext<SecurityDBContext>(p => p.UseSqlServer(IConfig.GetConnectionString("SecurityDBBanco"),
                a => { a.MigrationsAssembly(typeof(SecurityDBContext).Assembly.FullName);
                    a.EnableRetryOnFailure();
                }));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<SecurityDBContext>();
            services.AddTransient<IAuthService, AuthService>();
            var tokenValidationParameter = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(IConfig["JwtSettings:Key"]))
            };
            services.AddSingleton(tokenValidationParameter);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(c => { c.SaveToken = true;
                c.TokenValidationParameters = tokenValidationParameter;
            });
            return services;
        }
    }
}
