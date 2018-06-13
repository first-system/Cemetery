﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cemetery.Models.ViewModels
{
    public class Register
    {
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Display(Name ="Повторите пароль")]
        [DataType(DataType.Password)]
        [Required]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}