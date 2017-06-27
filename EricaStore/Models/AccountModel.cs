using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EricaStore.Models
{
    public class AccountModel
    {
        public MembershipTypeModel SelectedMembership { get; set; }
        public DayOfWeek[] DaysAvailable { get; set; }
    }
}