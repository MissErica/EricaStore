using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EricaStore.Models
{
    public class CartItemModel
    {
        public ProductsModel Product { get; set; }
        
        public int? Quantity { get; set; }
    }
}