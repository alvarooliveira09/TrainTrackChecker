using Microsoft.AspNetCore.Identity;

namespace TrainTrackChecker.Models {
    public class User : IdentityUser
    {
        public string? Image { get; set; }
        public ICollection<UserProfile>? UserProfiles { get; set; }
    }
}
