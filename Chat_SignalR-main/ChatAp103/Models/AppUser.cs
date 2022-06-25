using Microsoft.AspNetCore.Identity;

namespace ChatAp103.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
        public string ConnectionId { get; set; }
    }
}
