using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EricaStore.Models
{
    public class CheckoutModel
    {

        [Required]
        [Display(Name = "Name")]
        public string ShippingName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string ShippingEmail { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Shipping Address")]
        [Required]
        public string ShippingAddress1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string ShippingAddress2 { get; set; }

        [Required]
        [Display(Name = "City")]
        public string ShippingCity { get; set; }

        [Required]
        [Display(Name = "State")]
        public string ShippingState { get; set; }

        [Required]
        [MinLength(5)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }


    }
}