using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EricaStore.Models
{
    public class CartModel
    {
        public decimal? Subtotal { get; set; }
        public CartItemModel[] Items { get; set; }

    }
} 