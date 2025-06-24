﻿using System.ComponentModel.DataAnnotations;

namespace ForumProject.Models
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}