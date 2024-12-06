using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TrainTrackChecker.Models {
    public class Trecho_Registro : AuditableEntity {

        [ForeignKey("TrechoId")]
        public virtual Trecho? Trecho { get; set; } = default!;
        public Guid? TrechoId { get; set; }

        [Required(ErrorMessage = "Por favor preencha a 'Data/Hora' do registro de leitura.")]
        public DateTime? DataHora { get; set; }

        [Required(ErrorMessage = "Por favor preencha o 'Distância' da leitura realizada.")]
        public int? DistanciaMM { get; set; }

        [Required(ErrorMessage = "Por favor preencha a 'Coordenada' geográfica da leitura.")]
        public string? Coordenada { get; set; }

        public bool Verificado { get; set; } = false;

        public EnvioApiStatus StatusEnvioAPI { get; set; } = EnvioApiStatus.Aguardando;


    }
}
