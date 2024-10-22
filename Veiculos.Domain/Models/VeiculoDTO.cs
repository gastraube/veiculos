using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veiculos.Domain.Models
{
    public class VeiculoDTO
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
        public string Modelo { get; set; }
        public int? TipoVeiculo { get; set; }
        public int? CapacidadeCarga { get; set; }
        public int? CapacidadePassageiro { get; set; }
    }
}
