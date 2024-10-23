using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veiculos.Domain.Models
{
    public class RevisaoDTO
    {
        public int Id { get; set; }
        public int Km { get; set; }
        public DateTime Data { get; set; }
        public double ValorDaRevisao { get; set; }
        public int? VeiculoId { get; set; }
    }
}
