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
        public DateTime? CreditCardExpiration { get; set; }
        [Required]
        [CreditCard]
        public string CreditCardNumber { get; set; }
        public string CreditCardName { get; set; }
        public string CreditCardVerificationValue { get; set; }

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