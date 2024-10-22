using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Veiculos.Domain.Model
{
    public class Caminhao
    {
        public int Id { get; set; }
        public int CapacidadeCarga { get; set; }
        [JsonIgnore]
        public Veiculo Veiculo { get; set; }
    }
}
