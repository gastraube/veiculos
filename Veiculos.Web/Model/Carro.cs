using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Veiculos.Web.Model
{
    public class Carro
    {
        [ForeignKey("CarroId")]
        public int Id { get; set; }
        public int CapacidadePassageiro { get; set; }
        [JsonIgnore]
        public Veiculo Veiculo { get; set; }
    }
}
