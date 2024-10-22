
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

        public VeiculoService(IVeiculoRepository veiculoRepository,
            ICarroRepository carroRepository,
            ICaminhaoRepository caminhaoRepository,
            IMapper mapper
            )
        {
            _veiculoRepository = veiculoRepository;
            _caminhaoRepository = caminhaoRepository;
            _carroRepository = carroRepository;
            _mapper = mapper;
        }

        public async Task<List<VeiculoDTO>> GetAllVeiculos()
        {
            var veiculos = await _veiculoRepository.GetAllAsync(true, "Carro", "Caminhao");            
            return _mapper.Map<List<VeiculoDTO>>(veiculos); ;
        }

        public async Task<VeiculoDTO> GetVeiculoById(int id)
        {
            var veiculo = await _veiculoRepository.GetByIdAsync(id);

            var veiculoDTO = _mapper.Map<VeiculoDTO>(veiculo);

            if (veiculo.Carro != null)
            {   
                veiculoDTO.CapacidadePassageiro = veiculo.Carro.CapacidadePassageiro;
                veiculoDTO.TipoVeiculo = (int)TipoVeiculo.Carro;
            }
            else if (veiculo.Caminhao != null)
            {
                veiculoDTO.CapacidadeCarga = veiculo.Caminhao.CapacidadeCarga;
                veiculoDTO.TipoVeiculo = (int)TipoVeiculo.Caminhao;
            }
            else
                veiculoDTO.TipoVeiculo = 0;

            return veiculoDTO;
        }

        public async Task<VeiculoDTO?> UpdateVeiculo(VeiculoDTO veiculo)
        {
            var veiculoBD = await _veiculoRepository.GetByIdAsync(veiculo.Id);

            if (veiculoBD != null)
            {
                veiculoBD.Ano = veiculo.Ano;
                veiculoBD.Placa = veiculo.Placa;
                veiculoBD.Modelo = veiculo.Modelo;
                veiculoBD.Cor = veiculo.Cor;

                if (veiculo.TipoVeiculo == (int)TipoVeiculo.Carro)
                {
                    veiculoBD.Carro.CapacidadePassageiro = veiculo.CapacidadePassageiro.Value;
                    veiculoBD.Caminhao = null;
                }

                if (veiculo.TipoVeiculo == (int)TipoVeiculo.Caminhao)
                {
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
                if (veiculo.Carro != null)
                    await _carroRepository.DeleteByIdAsync(veiculo.CarroId.Value);

                if (veiculo.Caminhao != null)
                    await _caminhaoRepository.DeleteByIdAsync(veiculo.CaminhaoId.Value);

                await _veiculoRepository.DeleteByIdAsync(veiculo.Id);
                return true;
            }
            return false;
        }
    }
}

