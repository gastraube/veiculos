using Microsoft.EntityFrameworkCore;
using Veiculos.Web.Entity;
using Veiculos.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    // this defines a CORS policy called "default"
    options.AddPolicy("default", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//dependency injection
builder.Services.AddSingleton<ICarroService, CarroService>();
builder.Services.AddSingleton<ICaminhaoService, CaminhaoService>();
builder.Services.AddSingleton<IVeiculoService, VeiculoService>();


builder.Services.AddDbContext<VeiculosDbContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("VeiculosConnectionString")), ServiceLifetime.Singleton);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

{
    var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<VeiculosDbContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}

app.UseCors("default");

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
