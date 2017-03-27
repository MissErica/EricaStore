﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EricaStore.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }


        [Required]
        [MinLength(7)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}





