using Veiculos.Web.Model;

namespace Veiculos.Web.Services
{
    public interface ICarroService
    {
        Task<List<Carro>> GetAllCarros();
        Task<Carro?> AddCarro(CarroAddOrUpdate obj);
        Task<Carro?> UpdateCarro(int id, Carro carro);
        Task<bool> DeleteCarroByID(int id);
    }
}
