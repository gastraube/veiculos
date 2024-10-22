using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Veiculos.Domain.Model;

namespace Veiculos.Domain.Entity
{
    public class VeiculosDbContext : DbContext
    {
        public VeiculosDbContext(DbContextOptions<VeiculosDbContext> options) : base(options)
        {
        }

        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Caminhao> Caminhoes { get; set; }
        public DbSet<Revisao> Revisoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Veiculo>()
                .HasOne(e => e.Carro);

            modelBuilder.Entity<Veiculo>()
               .HasOne(e => e.Caminhao);

            modelBuilder.Entity<Carro>()
                .HasOne(e => e.Veiculo)
                .WithOne(e => e.Carro)
                .HasForeignKey<Veiculo>(e => e.CarroId);

            modelBuilder.Entity<Caminhao>()
                .HasOne(e => e.Veiculo)
                .WithOne(e => e.Caminhao)
                .HasForeignKey<Veiculo>(e => e.CaminhaoId);

            modelBuilder.Entity<Revisao>()
                .HasOne(x => x.Veiculo);


            Carro[] carros = [
                new Carro()
                {
                    Id = 1,
                    CapacidadePassageiro = 4
                },
                new Carro()
                {
                    Id = 2,
                    CapacidadePassageiro = 4
                },
                new Carro()
                {
                    Id = 3,
                    CapacidadePassageiro = 4
                },
                new Carro()
                {
                    Id = 4,
                    CapacidadePassageiro = 4
                },
                new Carro()
                {
                    Id = 5,
                    CapacidadePassageiro = 4
                }
            ];


            Caminhao[] caminhoes = [
                new Caminhao()
                {
                    Id = 1,
                    CapacidadeCarga = 400
                },
                new Caminhao()
                {
                    Id = 2,
                    CapacidadeCarga = 500
                },
                new Caminhao()
                {
                    Id = 3,
                    CapacidadeCarga = 700
                },
                new Caminhao()
                {
                    Id = 4,
                    CapacidadeCarga = 1000
                },
                new Caminhao()
                {
                    Id = 5,
                    CapacidadeCarga = 200
                }
            ];


            Veiculo[] veiculos = [
                new Veiculo(){
                    Id = 1,
                    Ano = 2014,
                    Cor = "Preto",
                    Placa = "RTY-9876",
                    Modelo = "Citroen",
                    CarroId = 1
                },               
                new Veiculo(){
                    Id = 2,
                    Ano = 2009,
                    Cor = "Verde",
                    Placa = "HJS-1245",
                    Modelo = "Honda",
                    CarroId = 2
                },
                new Veiculo(){
                    Id = 3,
                    Ano = 2024,
                    Cor = "Prata",
                    Placa = "HAU-2938",
                    Modelo = "BYD",
                    CarroId = 3
                },
                new Veiculo(){
                    Id = 4,
                    Ano = 2020,
                    Cor = "Preto",
                    Placa = "DGF-0009",
                    Modelo = "BMW",
                    CarroId = 4
                },
                new Veiculo(){
                    Id = 5,
                    Ano = 2024,
                    Cor = "Amarelo",
                    Placa = "HJA-1542",
                    Modelo = "Fusca",
                    CarroId = 5
                },
                new Veiculo(){
                    Id=6,
                    Ano = 1993,
                    Cor = "Prata",
                    Placa = "UHA-2176",
                    Modelo = "Corsa",
                    CaminhaoId = 1
                },
                new Veiculo(){
                    Id = 7,
                    Ano = 2003,
                    Cor = "Branco",
                    Placa = "GDG-1223",
                    Modelo = "Scania",
                    CaminhaoId = 2
                },
                new Veiculo(){
                    Id = 8,
                    Ano = 1997,
                    Cor = "Vermelho",
                    Placa = "FSD-9900",
                    Modelo = "Wolksvagem",
                    CaminhaoId = 3
                },
                new Veiculo(){
                    Id = 9,
                    Ano = 2013,
                    Cor = "Azul",
                    Placa = "YDY-0000",
                    Modelo = "Mercedes-Benz",
                    CaminhaoId = 4
                },
                new Veiculo(){
                    Id = 10,
                    Ano = 2013,
                    Cor = "Cinza",
                    Placa = "UIK-5547",
                    Modelo = "DAF",
                    CaminhaoId = 5
                }
             ];

            Revisao[] revisoes = [
                new Revisao()
                {
                    Id = 1,
                    Data = new DateTime(2022, 1, 1),
                    Km = 20000,
                    ValorDaRevisao = 1000,
                    VeiculoId = 1
                },
                     new Revisao()
                {
                    Id = 2,
                    Data = new DateTime(2020, 12, 1),
                    Km = 100,
                    ValorDaRevisao = 300,
                    VeiculoId = 3
                },
                new Revisao()
                {
                    Id = 3,
                    Data = new DateTime(2019, 5, 16),
                    Km = 800,
                    ValorDaRevisao = 500,
                    VeiculoId = 5
                },
                new Revisao()
                {
                    Id = 4,
                    Data = new DateTime(2024, 4, 1),
                    Km = 2500,
                    ValorDaRevisao = 300,
                    VeiculoId = 7
                },
                new Revisao()
                {
                    Id = 5,
                    Data = new DateTime(2001, 6, 5),
                    Km = 40000,
                    ValorDaRevisao = 600,
                    VeiculoId = 9
                },
            ];


            modelBuilder.Entity<Carro>().HasData(carros);
            modelBuilder.Entity<Caminhao>().HasData(caminhoes);
            modelBuilder.Entity<Revisao>().HasData(revisoes);
            modelBuilder.Entity<Veiculo>().HasData(veiculos);


        }
    }
}
