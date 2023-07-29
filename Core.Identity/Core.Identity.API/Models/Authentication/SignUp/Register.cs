﻿using System.ComponentModel.DataAnnotations;

namespace Core.Identity.API.Models.Authentication.SignUp
{
    public class Register
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
