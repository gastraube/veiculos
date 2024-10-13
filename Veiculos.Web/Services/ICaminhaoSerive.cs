using Veiculos.Web.Model;

namespace Veiculos.Web.Services
{
    public interface ICaminhaoService
    {
        Task<List<Caminhao>> GetAllCaminhaos();
        Task<List<Caminhao>> GetAllCaminhoes();
        Task<Caminhao?> AddCaminhao(Caminhao Caminhao);
        Task<Caminhao?> UpdateCaminhao(int id, Caminhao Caminhao);
        Task<bool> DeleteCaminhaoByID(int id);
    }
}
