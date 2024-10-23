using AutoMapper;
using Veiculos.Domain.Model;
using Veiculos.Domain.Models;

namespace Veiculos.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Veiculo, VeiculoDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Placa, opt => opt.MapFrom(src => src.Placa))
            .ForMember(dest => dest.Ano, opt => opt.MapFrom(src => src.Ano))
            .ForMember(dest => dest.Cor, opt => opt.MapFrom(src => src.Cor))
            .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.Modelo));

            CreateMap<VeiculoDTO, Veiculo>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Placa, opt => opt.MapFrom(src => src.Placa))
              .ForMember(dest => dest.Ano, opt => opt.MapFrom(src => src.Ano))
              .ForMember(dest => dest.Cor, opt => opt.MapFrom(src => src.Cor))
              .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.Modelo));


            CreateMap<RevisaoDTO, Revisao>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Km, opt => opt.MapFrom(src => src.Km))
              .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
              .ForMember(dest => dest.ValorDaRevisao, opt => opt.MapFrom(src => src.ValorDaRevisao))
              .ForMember(dest => dest.VeiculoId, opt => opt.MapFrom(src => src.VeiculoId));
        }
    }
}
