using Microsoft.AspNetCore.Identity;

namespace Sample.Models
{
    public class User:IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

    }
}
