using System;
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
        [MinLength(7)]
        public string Password { get; set; }

        [Compare("NewPassword", ErrorMessage ="Password doesn't match")]
        public string ConfirmPassWord { get; set; }



    }
}