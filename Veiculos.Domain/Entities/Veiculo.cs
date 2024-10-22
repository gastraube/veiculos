using System.ComponentModel.DataAnnotations.Schema;

namespace Veiculos.Domain.Model
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
        public string Modelo { get; set; }
        public int? CarroId { get; set; }
        public Carro? Carro { get; set; }
        public int? CaminhaoId { get; set; }
        public Caminhao? Caminhao { get; set; }
    }
}
