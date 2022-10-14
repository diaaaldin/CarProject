﻿using System;

namespace CarProject.ViewModel
{
    public class UserRegisterViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int? IsAdmin { get; set; }

    }
}