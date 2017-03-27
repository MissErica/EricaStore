using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EricaStore.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public int Rating { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }

        public virtual Product Product { get; set; }
    }
}