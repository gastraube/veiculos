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

builder.Services.AddDbContext<VeiculosDbContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("VeiculosConnectionString")), ServiceLifetime.Transient);

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<IMapper, Mapper>();
builder.Services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));    
builder.Services.AddTransient<IRevisaoRepository, RevisaoRepository>();
builder.Services.AddTransient<IRevisaoService, RevisaoService>();
builder.Services.AddTransient<ICaminhaoRepository, CaminhaoRepository>();
builder.Services.AddTransient<ICarroRepository, CarroRepository>();
builder.Services.AddTransient<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddTransient<IVeiculoService, VeiculoService>();

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
