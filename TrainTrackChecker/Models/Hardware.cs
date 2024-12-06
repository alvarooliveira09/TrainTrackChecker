using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TrainTrackChecker.Models {

    public class Hardware : AuditableEntity {

        public int Codigo { get; set; }
        [Required(ErrorMessage = "Por favor preencha o 'nome' do hardware.")]
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public bool EmOperacao { get; set; } = false;

        [Required(ErrorMessage = "Por favor preencha o 'número de série' do hardware.")]
        public string NumeroSerie { get; set; } = string.Empty;
        public string? LocalInstalacao { get; set; }
        public DateTime? DataHoraInstalacao { get; set; }

        public string? UltimaLocalizacaoCoordenada { get; set; }
        public DateTime? UltimaLocalizacaoDataHora { get; set; }

    }

}
