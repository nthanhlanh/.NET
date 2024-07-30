using ContosoPizza.Models;
using Microsoft.AspNetCore.Identity;

namespace ContosoPizza.Helprs
{
    public class ApplicationUser : IdentityUser
    {
        // Thêm thuộc tính tùy chỉnh nếu cần
        public ICollection<UserGroup>? UserGroups { get; set; }
    }
}

