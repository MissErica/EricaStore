using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EricaStore.Models
{
    public partial class OrderProductModel
    {
       public int OrderId { get; set; }
        public int ProductID { get; set; }
        public int MembershipTypeID { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

    }
}