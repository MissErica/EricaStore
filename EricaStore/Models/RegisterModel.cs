﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EricaStore.Models
{
    public class RegisterModel
    {   
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6) ]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }

        //[Compare("NewPassword", ErrorMessage ="Password doesn't match")]
        //public string ConfirmPassWord { get; set; }



    }
}