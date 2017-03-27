using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EricaStore.Models
{
    public class ProductImageModel
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public string Path { get; set; }
        public string AltText { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }

        public virtual Product Product { get; set; }
    }
}