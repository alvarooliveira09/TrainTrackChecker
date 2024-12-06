using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TrainTrackChecker.Models {

    public enum OrdemManutencao_LocalStatus { Aguardando = 0, Realizada = 1, NãoRealizada = 2 }
    public class OrdemManutencao_Local : AuditableEntity {
        
        [ForeignKey("OrdemManutencaoId")]
        public virtual OrdemManutencao? OrdemManutencao { get; set; } = default!;
        public Guid? OrdemManutencaoId { get; set; }

        public OrdemManutencao_LocalStatus Status { get; set; } = OrdemManutencao_LocalStatus.Aguardando;

        [Required(ErrorMessage = "Por favor preencha a 'Coordenada' geográfica do local a ser realizada a manutenção.")]
        public string? Coordenada { get; set; }

        [Required(ErrorMessage = "Por favor preencha o 'Data/Hora' da manutenção realizada.")]
        public DateTime? DataHora { get; set; } //Nullable somente aqui e não na API


        
        [ForeignKey("UserId")]
        public virtual User? User { get; set; } = default!;
        public string? UserId { get; set; }

        public EnvioApiStatus StatusEnvioAPI { get; set; } = EnvioApiStatus.Aguardando;

    }
}
