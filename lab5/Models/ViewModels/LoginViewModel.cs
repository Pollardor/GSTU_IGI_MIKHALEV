﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember you?")]
        public bool RememberMe { get; set; }
    }
}
