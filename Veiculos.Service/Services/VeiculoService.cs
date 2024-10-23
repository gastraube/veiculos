
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Veiculos.Data.Repositories;
using Veiculos.Data.Repositories.Abstractions;
using Veiculos.Domain.Entity;
using Veiculos.Domain.Model;
using Veiculos.Domain.Models;
using Veiculos.Service.Services.Abstraction;


namespace Veiculos.Service.Services
{
    public class VeiculoService : IVeiculoService
    {       
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly ICarroRepository _carroRepository;
        private readonly ICaminhaoRepository _caminhaoRepository;
        private readonly IMapper _mapper;
        private readonly IRevisaoService _revisaoService;

        public VeiculoService(IVeiculoRepository veiculoRepository,
            ICarroRepository carroRepository,
            ICaminhaoRepository caminhaoRepository,
            IMapper mapper,
            IRevisaoService revisaoService
            )
        {
            _veiculoRepository = veiculoRepository;
            _caminhaoRepository = caminhaoRepository;
            _carroRepository = carroRepository;
            _mapper = mapper;
            _revisaoService = revisaoService;
        }

        public async Task<List<VeiculoDTO>> GetAllVeiculos()
        {
            var veiculos = await _veiculoRepository.GetAllAsync(null, true, "Carro", "Caminhao");            
            return _mapper.Map<List<VeiculoDTO>>(veiculos); ;
        }

        public async Task<VeiculoDTO> GetVeiculoById(int id)
        {
            var veiculo = await _veiculoRepository.GetByIdAsync(id);

            var veiculoDTO = _mapper.Map<VeiculoDTO>(veiculo);

            if (veiculo.CarroId != null)
            {   
                var carro = await _carroRepository.GetByIdAsync(veiculo.CarroId.Value);
                veiculoDTO.CapacidadePassageiro = carro.CapacidadePassageiro;
                veiculoDTO.TipoVeiculo = (int)TipoVeiculo.Carro;
            }
            else if (veiculo.CaminhaoId != null)
            {
                var caminhao = await _caminhaoRepository.GetByIdAsync(veiculo.CaminhaoId.Value);
                veiculoDTO.CapacidadeCarga = caminhao.CapacidadeCarga;
                veiculoDTO.TipoVeiculo = (int)TipoVeiculo.Caminhao;
            }
            else
                veiculoDTO.TipoVeiculo = 0;

            return veiculoDTO;
        }

        public async Task<VeiculoDTO?> UpdateVeiculo(VeiculoDTO veiculo)
        {
            var veiculosBD = await _veiculoRepository.GetAllAsync(x => x.Id == veiculo.Id, true, "Carro", "Caminhao");

            var veiculoBD = veiculosBD.FirstOrDefault();

            if (veiculoBD != null)
            {
                veiculoBD.Ano = veiculo.Ano;
                veiculoBD.Placa = veiculo.Placa;
                veiculoBD.Modelo = veiculo.Modelo;
                veiculoBD.Cor = veiculo.Cor;

                if (veiculo.TipoVeiculo == (int)TipoVeiculo.Carro)
                { 
                    if (veiculoBD.Carro == null)
                        veiculoBD.Carro = new Carro();

                    veiculoBD.Carro.CapacidadePassageiro = veiculo.CapacidadePassageiro.Value;
                    veiculoBD.Caminhao = null;
                }

                if (veiculo.TipoVeiculo == (int)TipoVeiculo.Caminhao)
                {
                    if (veiculoBD.Caminhao == null)
                        veiculoBD.Caminhao = new Caminhao();

                    veiculoBD.Caminhao.CapacidadeCarga = veiculo.CapacidadeCarga.Value;
                    veiculoBD.Carro = null;
                }

                await _veiculoRepository.UpdateAsync(veiculoBD);
                return veiculo;
            }
            return null;
        }

        public async Task<Veiculo?> CreateVeiculo(VeiculoDTO veiculo)
        {
            var veiculoBD = new Veiculo();

            veiculoBD.Ano = veiculo.Ano;
            veiculoBD.Placa = veiculo.Placa;
            veiculoBD.Modelo = veiculo.Modelo;
            veiculoBD.Cor = veiculo.Cor;

            if (veiculo.TipoVeiculo == (int)TipoVeiculo.Carro)
            {
                veiculoBD.Carro = new Carro();
                veiculoBD.Carro.CapacidadePassageiro = veiculo.CapacidadePassageiro.Value;
                veiculoBD.Caminhao = null;
            }

            if (veiculo.TipoVeiculo == (int)TipoVeiculo.Caminhao)
            {
                veiculoBD.Caminhao = new Caminhao();
                veiculoBD.Caminhao.CapacidadeCarga = veiculo.CapacidadeCarga.Value;
                veiculoBD.Carro = null;
            }
            await _veiculoRepository.AddAsync(veiculoBD);
            return veiculoBD;
        }

        public async Task<bool> DeleteVeiculoByID(int id)
        {
            var veiculo = await _veiculoRepository.GetByIdAsync(id);

            if (veiculo != null)
            {
                await _veiculoRepository.DeleteByIdAsync(veiculo.Id);

                var revisoes = await _revisaoService.GetRevisoesByVeiculoId(id);

                if (revisoes != null)
                    await _revisaoService.DeleteRevisoes(revisoes);

                if (veiculo.CarroId != null)
                    await _carroRepository.DeleteByIdAsync(veiculo.CarroId.Value);

                if (veiculo.CaminhaoId != null)
                    await _caminhaoRepository.DeleteByIdAsync(veiculo.CaminhaoId.Value);                            
              
                return true;
            }
            return false;
        }
    }
}

