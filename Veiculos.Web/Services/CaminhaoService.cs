using Microsoft.EntityFrameworkCore;
using Veiculos.Web.Entity;
using Veiculos.Web.Model;

namespace Veiculos.Web.Services
{
    public class CaminhaoService : ICaminhaoService
    {
        private readonly VeiculosDbContext _db;
        public CaminhaoService(VeiculosDbContext db)
        {
            _db = db;
        }

        public async Task<List<Caminhao>> GetAllCaminhaos()
        {
            return await _db.Caminhoes.Include(x => x.Veiculo).ToListAsync();
        }

        public async Task<List<Caminhao>> GetAllCaminhoes()
        {
            return await _db.Caminhoes.ToListAsync();
        }

        public async Task<Caminhao?> AddCaminhao(Caminhao Caminhao)
        {
            _db.Caminhoes.Add(Caminhao);
            var result = await _db.SaveChangesAsync();
            return result >= 0 ? Caminhao : null;
        }

        public async Task<Caminhao?> UpdateCaminhao(int id, Caminhao Caminhao)
        {
            var CaminhaoBD = await _db.Caminhoes.Include(x => x.Veiculo).FirstOrDefaultAsync(index => index.Id == id);
            if (CaminhaoBD != null)
            {
                CaminhaoBD.CapacidadeCarga = Caminhao.CapacidadeCarga;
                CaminhaoBD.Veiculo.Ano = Caminhao.Veiculo.Ano;
                CaminhaoBD.Veiculo.Placa = Caminhao.Veiculo.Placa;
                CaminhaoBD.Veiculo.Modelo = Caminhao.Veiculo.Modelo;
                CaminhaoBD.Veiculo.Cor = Caminhao.Veiculo.Cor;

                var result = await _db.SaveChangesAsync();
                return result >= 0 ? CaminhaoBD : null;
            }
            return null;
        }

        public async Task<bool> DeleteCaminhaoByID(int id)
        {
            var Caminhao = await _db.Caminhoes.Include(x => x.Veiculo).FirstOrDefaultAsync(index => index.Id == id);

            if (Caminhao != null)
            {
                _db.Veiculos.Remove(Caminhao.Veiculo);
                _db.Caminhoes.Remove(Caminhao);
                var result = await _db.SaveChangesAsync();
                return result >= 0;
            }
            return false;
        }
    }
}
