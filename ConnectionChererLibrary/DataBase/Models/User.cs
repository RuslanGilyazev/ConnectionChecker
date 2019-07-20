using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ConnectionCheckerLibrary.DataBase.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}
