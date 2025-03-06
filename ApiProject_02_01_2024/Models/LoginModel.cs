using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiProject_02_01_2024.Models
{
    public class LoginModel
    {
        [EmailAddress]
        public string? Email { get; set; }
        [PasswordPropertyText]
        public string? Password { get; set; }
    }
}
