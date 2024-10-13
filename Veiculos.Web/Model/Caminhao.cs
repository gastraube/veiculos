using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Veiculos.Web.Model
{
    public class Caminhao
    {
        [ForeignKey("CaminhaoId")]
        public int Id { get; set; }
        public int CapacidadeCarga { get; set; }
        [JsonIgnore]
        public Veiculo Veiculo { get; set; }
    }
}
