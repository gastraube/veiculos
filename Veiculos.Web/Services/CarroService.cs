using Microsoft.EntityFrameworkCore;
using Veiculos.Web.Entity;
using Veiculos.Web.Model;

namespace Veiculos.Web.Services
{
    public class CarroService : ICarroService
    {
        private readonly VeiculosDbContext _db;
        public CarroService(VeiculosDbContext db)
        {
            _db = db;
        }

        public async Task<List<Carro>> GetAllCarros()
        {
            return await _db.Carros.Include(x => x.Veiculo).ToListAsync();
        }

        public async Task<List<Caminhao>> GetAllCaminhoes()
        {
            return await _db.Caminhoes.ToListAsync();
        }

        public async Task<Carro?> AddCarro(CarroAddOrUpdate carroNovo)
        {
            var carro = new Carro();
            carro.CapacidadePassageiro = carroNovo.CapacidadePassageiro;

            carro.Veiculo = new Veiculo();
            carro.Veiculo.Modelo = carroNovo.Modelo;
            carro.Veiculo.Placa = carroNovo.Placa;
            carro.Veiculo.Cor = carroNovo.Cor;
            carro.Veiculo.Ano = carroNovo.Ano;

            _db.Carros.Add(carro);
            var result = await _db.SaveChangesAsync();
            return result >= 0 ? carro : null;
        }

        public async Task<Carro?> UpdateCarro(int id, Carro carro)
        {
            var carroBD = await _db.Carros.Include(x => x.Veiculo).FirstOrDefaultAsync(index => index.Id == id);
            if (carroBD != null)
            {
                carroBD.CapacidadePassageiro = carro.CapacidadePassageiro;
                carroBD.Veiculo.Ano = carro.Veiculo.Ano;
                carroBD.Veiculo.Placa = carro.Veiculo.Placa;
                carroBD.Veiculo.Modelo = carro.Veiculo.Modelo;
                carroBD.Veiculo.Cor = carro.Veiculo.Cor;

                var result = await _db.SaveChangesAsync();
                return result >= 0 ? carroBD : null;
            }
            return null;
        }

        public async Task<bool> DeleteCarroByID(int id)
        {
            var carro = await _db.Carros.Include(x => x.Veiculo).FirstOrDefaultAsync(index => index.Id == id);

            if (carro != null)
            {
                _db.Veiculos.Remove(carro.Veiculo);
                _db.Carros.Remove(carro);
                var result = await _db.SaveChangesAsync();
                return result >= 0;
            }
            return false;
        }
    }
}
