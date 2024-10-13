using Veiculos.Web.Model;

namespace Veiculos.Web.Services
{
    public interface IVeiculoService
    {
        Task<List<Veiculo>> GetAllVeiculos();
        Task<Veiculo> GetVeiculoById(int id);
        Task<Veiculo?> UpdateVeiculo(VeiculoAddOrUpdate veiculo);
        Task<Veiculo?> CreateVeiculo(VeiculoAddOrUpdate veiculo);

        Task<bool> DeleteVeiculoByID(int id);
    }
}
