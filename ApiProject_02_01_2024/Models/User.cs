using Microsoft.AspNetCore.Identity;

namespace ApiProject_02_01_2024.Models
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
