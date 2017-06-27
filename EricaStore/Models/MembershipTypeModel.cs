using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EricaStore.Models
{
    public class MembershipTypeModel
    {
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        public string DaysString { get; set; }
        public DayOfWeek[] DaysOfWeek { get; set; }
        public IEnumerable<ProductsModel> Products { get; set; }
    }
}