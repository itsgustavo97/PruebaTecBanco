using Application.Contracts.Infrastructure;
using Infrastructure.Persistencia;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepositoryGeneric<>), typeof(RepositoryGeneric<>));
            services.AddTransient<IReportService, ReportService>();
            return services;
        }
    }
}
