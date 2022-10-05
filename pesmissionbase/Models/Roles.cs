using Microsoft.AspNetCore.Identity;

namespace pesmissionbase.Models
{
    public class Roles : IdentityRole
    {
        public string description { get; set; }
        public ICollection<UsersPermisions> UsersPermisions { get; set; }
    }
}
