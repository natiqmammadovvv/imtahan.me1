using Microsoft.AspNetCore.Identity;

namespace imtahan.me.Models
{
    public class AppUser: IdentityUser
    {
        public string Name { get; set; }

    }
}
