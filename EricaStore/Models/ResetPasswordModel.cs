using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EricaStore.Models
{
    public class ResetPasswordModel
    {
        public string EmailId { get; set; }

        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage="Password doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}