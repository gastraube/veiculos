using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veiculos.Data.Repositories;
using Veiculos.Data.Repositories.Abstractions;
using Veiculos.Domain.Model;
using Veiculos.Domain.Models;
using Veiculos.Service.Services.Abstraction;

namespace Veiculos.Service.Services
{
    public class RevisaoService : IRevisaoService
    {
        private readonly IRevisaoRepository _revisaoRepository;
        private readonly IMapper _mapper;

        public RevisaoService(IRevisaoRepository revisaoRepository, IMapper mapper)
        {
            _revisaoRepository = revisaoRepository;
            _mapper = mapper;
        }
        public async Task<Revisao> CreateRevisao(RevisaoDTO revisao)
        {
            var revisaoDB = _mapper.Map<Revisao>(revisao);
            await _revisaoRepository.AddAsync(revisaoDB);
            return revisaoDB;
        }

        public async Task<List<Revisao>> CreateRevisoes(List<RevisaoDTO> revisoesDto, int veiculoId)
        {
            var revisoesDB = await _revisaoRepository.GetAllAsync(x => x.VeiculoId == veiculoId, true);

            if (revisoesDB != null && revisoesDB.Count > 0)
                await DeleteRevisoes(veiculoId, revisoesDto, revisoesDB);
            
            await AddRevisoes(revisoesDto);

            return _mapper.Map<List<Revisao>>(revisoesDto);
        }

        public async Task DeleteRevisoes(int veiculoId, List<RevisaoDTO> revisoesDto, List<Revisao> revisoesDB)
        {
            foreach (var r in revisoesDB)
            {
                if (r.Id != 0 && revisoesDto.FirstOrDefault(x => x.Id == r.Id) == null)
                {
                    await _revisaoRepository.DeleteByIdAsync(r.Id);
                }
            }
        }

        public async Task AddRevisoes(List<RevisaoDTO> revisoes)
        {
            var revisoesDB = _mapper.Map<List<Revisao>>(revisoes);

            foreach (var r in revisoes)
            {
                if (r.Id != 0)
                {
                    revisoesDB.RemoveAll(x => x.Id == r.Id);
                }
            }            
            await _revisaoRepository.AddRangeAsync(revisoesDB);
        }

        public async Task<List<Revisao>> GetRevisoesByVeiculoId(int veiculoId)
        {
            var revisoes = await _revisaoRepository.GetAllAsync(x => x.VeiculoId == veiculoId, true);
            return revisoes;
        }

        public async Task DeleteRevisoes(List<Revisao> revisoes)
        {
            await _revisaoRepository.DeleteRange(revisoes);
        }
    }
}
