using System.ComponentModel.DataAnnotations;

namespace TrainTrackChecker.Models {
    public class EntityBase {
        [Key]
        public Guid Id { get; set; }


    }
}
