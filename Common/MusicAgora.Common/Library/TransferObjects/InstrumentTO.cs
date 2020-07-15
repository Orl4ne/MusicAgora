using System.Collections.Generic;
using Identity.DAL;

namespace MusicAgora.Common.Library.TransferObjects
{
    public class InstrumentTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> LibUserIds { get; set; }
        //public List<LibUserTO> LibUsers { get; set; }
        //public List<ApplicationUser> IdentityUsers { get; set; }
    }
}