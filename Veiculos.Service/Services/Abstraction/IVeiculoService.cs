

using Veiculos.Domain.Model;
using Veiculos.Domain.Models;

namespace Veiculos.Service.Services.Abstraction
{
    public interface IVeiculoService
    {
        Task<List<VeiculoDTO>> GetAllVeiculos();
        Task<VeiculoDTO> GetVeiculoById(int id);
        Task<VeiculoDTO?> UpdateVeiculo(VeiculoDTO veiculo);
        Task<Veiculo?> CreateVeiculo(VeiculoDTO veiculo);
        Task<bool> DeleteVeiculoByID(int id);
    }
}
