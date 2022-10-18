﻿using System.ComponentModel;

namespace CarProject.ModelViews.ViewModel
{
    public class UserLoginResponseViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [DefaultValue("")]
        public string Image { get; set; }
        public string Token { get; set; }
    }
}
