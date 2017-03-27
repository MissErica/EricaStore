using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EricaStore.Models
{
    public class IdentityModels : IdentityDbContext<User>
    {
        public IdentityModels() 
            : base("name=EricaStore")
        {

        }

    }

    public class User : IdentityUser
    {

    }
}