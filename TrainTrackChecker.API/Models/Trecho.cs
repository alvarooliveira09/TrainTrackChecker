using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TrainTrackChecker.API.Models {

    public enum TrechoStatus { LeituraEmAndamento = 0, LeituraFinalizada = 1 }
    public enum EnvioApiStatus { Aguardando = 0, EmAndamento = 1, Enviado = 2 }

    public class Trecho : AuditableEntity {

        public int Codigo { get; set; }

        public TrechoStatus Status { get; set; } = TrechoStatus.LeituraEmAndamento;

        [ForeignKey("HardwareId")]
        public virtual Hardware? Hardware { get; set; } = default!;
        public Guid? HardwareId { get; set; }

        public string? NumeroSerieHardware { get; set; }

        [Required(ErrorMessage = "Por favor preencha a 'Data/Hora' do início de leitura de trecho.")]
        public DateTime? DataHoraInicio { get; set; }
        public DateTime? DataHoraFim { get; set; }

        public bool Verificado { get; set; } = false;
        public EnvioApiStatus StatusEnvioAPI { get; set; } = EnvioApiStatus.Aguardando;

    }

}
