using ContosoPizza.Helprs;

namespace ContosoPizza.Models
{
    public class UserGroup
    {
        public required string UserId { get; set; } 
        public ApplicationUser? User { get; set; }

        public int GroupId { get; set; }
        public Group? Group { get; set; }
    }
}
