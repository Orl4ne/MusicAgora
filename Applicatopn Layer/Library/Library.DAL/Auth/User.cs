using Library.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.DAL.Auth
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public AccessRight AccessRight { get; set; }
        public List<UserInstrumentEF> UserInstrument { get; set; }
        public bool IsIndependance { get; set; }
        public bool IsGarde { get; set; }
    }
}
