using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EricaStore.Models
{
    public class ProductsModel 
    {
        public int? Id { get; set; }


        public string ProductName { get; set; }

        public IEnumerable<string> Image { get; set; }

       
        public string Category { get; set; }

        public string Ingredients { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        
    }
}