using System.ComponentModel.DataAnnotations;

namespace TrainTrackChecker.API.Models {
    public class EntityBase {
        [Key]
        public Guid Id { get; set; }


    }
}
