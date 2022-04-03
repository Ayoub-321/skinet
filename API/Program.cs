using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using API.Helpers;

var builder = WebApplication.CreateBuilder(args);
string connString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(x =>
{
    x.UseSqlite(connString);
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<StoreContext>();
    var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
    dataContext.Database.Migrate();
    await StoreContextSeed.SeedAsync(dataContext, loggerFactory);
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
