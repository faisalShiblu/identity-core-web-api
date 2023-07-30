using System.ComponentModel.DataAnnotations;

namespace Core.Identity.API.Models.Authentication.LogIn
{
    public class LogIn
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
