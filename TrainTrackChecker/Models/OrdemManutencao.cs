using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainTrackChecker.Models {

    public enum OrdemManutencaoStatus { Cadastrada = 0, Aguardando = 1, Realizada = 2, NãoRealizada = 3 }

    public class OrdemManutencao : AuditableEntity {

        public int Codigo { get; set; }
        public OrdemManutencaoStatus Status { get; set; } = OrdemManutencaoStatus.Cadastrada;

        [Required(ErrorMessage = "Por favor preencha o 'Data/Hora' da Ordem de Manutencão.")]
        public DateTime? DataHora { get; set; }


        
        [ForeignKey("UserId")]
        public virtual User? User { get; set; } = default!;
        public string? UserId { get; set; } = string.Empty;

        public string ObsGerente { get; set; } = string.Empty;
        public string ObsOperador { get; set; } = string.Empty;

        public EnvioApiStatus StatusEnvioAPI { get; set; } = EnvioApiStatus.Aguardando;

    }

}
