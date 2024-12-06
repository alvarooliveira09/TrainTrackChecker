
namespace TrainTrackChecker.API.Models {
    public class UserProfile : EntityBase
    {
        public UserProfile()
        {
        }
        public string? UserId { get; set; }
        public User? ApplicationUser { get; set; }
    }
}
