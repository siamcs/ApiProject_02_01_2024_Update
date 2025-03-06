using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiProject_02_01_2024.DTOs
{
    public class LoginModelVM
    {
        [EmailAddress]
        public string? Email { get; set; }
        [PasswordPropertyText]
        public string? Password { get; set; }
    }
}
