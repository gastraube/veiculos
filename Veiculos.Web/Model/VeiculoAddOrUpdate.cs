using System.Text.Json.Serialization;

namespace Veiculos.Web.Model
{
    public class VeiculoAddOrUpdate
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
        public string Modelo { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoVeiculo TipoVeiculo { get; set; }
        public int? CapacidadeCarga { get; set; }
        public int? CapacidadePassageiro { get; set; }
    }
}
