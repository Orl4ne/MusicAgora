using Identity.DAL;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicAgora.WebUx.MVC.Models
{
    public class UserVM
    {
        public LibUserTO LibUser { get; set; }
        public ApplicationUser IdentityUser { get; set; }
    }
}
