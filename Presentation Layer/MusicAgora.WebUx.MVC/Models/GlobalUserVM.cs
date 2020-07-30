using Identity.DAL;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicAgora.WebUx.MVC.Models
{
    public class GlobalUserVM
    {
        public ApplicationUser IdentityUser { get; set; }
        public List<string> Roles { get; set; }
        public LibUserTO LibraryUser { get; set; }
    }
}
