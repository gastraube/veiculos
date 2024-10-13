using System.ComponentModel.DataAnnotations.Schema;

namespace Veiculos.Web.Model
{
    public class Revisao
    {
        [ForeignKey("RevisaoId")]
        public int Id { get; set; }
        public int Km { get; set; }
        public DateTime Data { get; set; }
        public double ValorDaRevisao { get; set; }
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }
    }
}
