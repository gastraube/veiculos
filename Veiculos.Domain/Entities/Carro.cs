using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Veiculos.Domain.Model
{
    public class Carro
    {     
        public int Id { get; set; }
        public int CapacidadePassageiro { get; set; }
        [JsonIgnore]
        public Veiculo Veiculo { get; set; }
    }
}
