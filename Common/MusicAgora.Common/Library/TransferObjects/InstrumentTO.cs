using System.Collections.Generic;
using Identity.DAL;

namespace MusicAgora.Common.Library.TransferObjects
{
    public class InstrumentTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> UserIds { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}