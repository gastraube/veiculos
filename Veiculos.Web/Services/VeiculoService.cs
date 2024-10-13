using Microsoft.EntityFrameworkCore;
using Veiculos.Web.Entity;
using Veiculos.Web.Model;

namespace Veiculos.Web.Services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly VeiculosDbContext _db;
        public VeiculoService(VeiculosDbContext db)
        {
            _db = db;
        }

        public async Task<List<Veiculo>> GetAllVeiculos()
        {
            return await _db.Veiculos
                 .Include(x => x.Carro)
                .Include(x => x.Caminhao)
                .ToListAsync();
        }

        public async Task<Veiculo> GetVeiculoById(int id)
        {
            var veiculo = await _db.Veiculos
                .Include(x => x.Carro)
                .Include(x => x.Caminhao)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            if (veiculo.Carro != null)
                veiculo.TipoVeiculo = TipoVeiculo.Carro;
            else if (veiculo.Caminhao != null)
                veiculo.TipoVeiculo = TipoVeiculo.Caminhao;
            else
                veiculo.TipoVeiculo = 0;

            return veiculo;
        }
        public async Task<Veiculo?> UpdateVeiculo(VeiculoAddOrUpdate veiculo)
        {
            var veiculoBD = await _db.Veiculos
                .Include(x => x.Carro)
                .Include(x => x.Caminhao)
                .FirstOrDefaultAsync(index => index.Id == veiculo.Id);

            if (veiculoBD != null)
            {
                veiculoBD.Ano = veiculo.Ano;
                veiculoBD.Placa = veiculo.Placa;
                veiculoBD.Modelo = veiculo.Modelo;
                veiculoBD.Cor = veiculo.Cor;

                if (veiculo.TipoVeiculo == TipoVeiculo.Carro)
                {
                    veiculoBD.Carro.CapacidadePassageiro = veiculo.CapacidadePassageiro.Value;
                    veiculoBD.Caminhao = null;
                }

                if (veiculo.TipoVeiculo == TipoVeiculo.Caminhao)
                {
                    veiculoBD.Caminhao.CapacidadeCarga = veiculo.CapacidadeCarga.Value;
                    veiculoBD.Carro = null;
                }

                var result = await _db.SaveChangesAsync();
                return result >= 0 ? veiculoBD : null;
            }
            return null;
        }

        public async Task<Veiculo?> CreateVeiculo(VeiculoAddOrUpdate veiculo)
        {
            var veiculoBD = new Veiculo();

            veiculoBD.Ano = veiculo.Ano;
            veiculoBD.Placa = veiculo.Placa;
            veiculoBD.Modelo = veiculo.Modelo;
            veiculoBD.Cor = veiculo.Cor;

            if (veiculo.TipoVeiculo == TipoVeiculo.Carro)
            {
                veiculoBD.Carro = new Carro();
                veiculoBD.Carro.CapacidadePassageiro = veiculo.CapacidadePassageiro.Value;
                veiculoBD.Caminhao = null;
            }

            if (veiculo.TipoVeiculo == TipoVeiculo.Caminhao)
            {
                veiculoBD.Caminhao = new Caminhao();
                veiculoBD.Caminhao.CapacidadeCarga = veiculo.CapacidadeCarga.Value;
                veiculoBD.Carro = null;
            }

            _db.Veiculos.Add(veiculoBD);
            var result = await _db.SaveChangesAsync();
            return result >= 0 ? veiculoBD : null;
        }

        public async Task<bool> DeleteVeiculoByID(int id)
        {
            var veiculo = await _db.Veiculos.Include(x => x.Caminhao).Include(x => x.Carro).FirstOrDefaultAsync(index => index.Id == id);

            if (veiculo != null)
            {
                _db.Veiculos.Remove(veiculo);
                var result = await _db.SaveChangesAsync();
                return result >= 0;
            }
            return false;
        }
    }
}

