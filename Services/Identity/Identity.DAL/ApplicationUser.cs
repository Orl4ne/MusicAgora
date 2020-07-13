using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Identity.DAL
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser() : base()
        { }

        public ApplicationUser(string userName) : base(userName)
        { }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public AccessRight AccessRight { get; set; }
        public List<int> InstrumentIds { get; set; }
        public bool IsIndependance { get; set; }
        public bool IsGarde { get; set; }
    }
}
