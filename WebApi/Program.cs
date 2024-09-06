using Application;
using Infrastructure;
using Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(conf =>
{
    conf.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Prueba t�cnica .NET",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Email = "gustavo.a.pineda01@gmail.com",
            Name = "Gustavo Adolfo Pineda"
        }
    });
});
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
			logger.LogError("Ocurri� un error: ",ex);
		}
    }
}

app.Run();
