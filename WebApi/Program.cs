using Application;
using Infrastructure;
using Infrastructure.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;
using System.Text.Json.Serialization;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
}).AddJsonOptions(p => p.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(conf =>
{
    conf.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Prueba técnica .NET",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Email = "gustavo.a.pineda01@gmail.com",
            Name = "Gustavo Adolfo Pineda"
        }
    });
    conf.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "Bearer",
        In = ParameterLocation.Header,
        Description = "Agregar la palabra Bearer, despues añade un espacio seguidamente el token",
    });
    conf.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                }
            },
            new string[]{ }
        }
    });
});
builder.Services.AddAuthentication();
builder.Services.InjectService(builder.Configuration); //Infrastructure
builder.Services.InjectServiceApp(); //Application
builder.Services.AddCors(c => 
{ 
	c.AddPolicy("politica", x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); 
});

QuestPDF.Settings.License = LicenseType.Community;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("politica");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
		var loggerFactory = services.GetRequiredService<ILoggerFactory>();
		try
		{
			var context = services.GetRequiredService<ApplicationDBContext>();
			await context.Database.MigrateAsync();
		}
		catch (Exception ex)
		{
			var logger = loggerFactory.CreateLogger<Program>();
			logger.LogError("Ocurrió un error: ",ex);
		}
    }
}

app.Run();
