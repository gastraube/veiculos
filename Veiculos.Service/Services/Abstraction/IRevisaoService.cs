using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veiculos.Domain.Model;
using Veiculos.Domain.Models;

namespace Veiculos.Service.Services.Abstraction
{
    public interface IRevisaoService
    {
        Task<List<Revisao>> GetRevisoesByVeiculoId(int veiculoId);
        Task<Revisao> CreateRevisao(RevisaoDTO veiculo);
        Task<List<Revisao>> CreateRevisoes(List<RevisaoDTO> revisoes, int veiculoId);
        Task DeleteRevisoes(List<Revisao> revisoes);
    }
}
