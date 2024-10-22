using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Veiculos.Data.Repositories;
using Veiculos.Data.Repositories.Abstractions;
using Veiculos.Domain.Entity;
using Veiculos.Service.Services;
using Veiculos.Service.Services.Abstraction;
using Veiculos.Web.Mapping;

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

builder.Services.AddDbContext<VeiculosDbContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("VeiculosConnectionString")), ServiceLifetime.Singleton);

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSingleton<IMapper, Mapper>();
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddSingleton<IRevisaoRepository, RevisaoRepository>();
builder.Services.AddSingleton<ICaminhaoRepository, CaminhaoRepository>();
builder.Services.AddSingleton<ICarroRepository, CarroRepository>();
builder.Services.AddSingleton<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddSingleton<IVeiculoService, VeiculoService>();

builder.Services.AddControllers();

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
