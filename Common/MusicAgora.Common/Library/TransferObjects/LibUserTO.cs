using System.Collections.Generic;

namespace MusicAgora.Common.Library.TransferObjects
{
    public class LibUserTO
    {
        public int LibUserId { get; set; }
        public int IdentityUserId { get; set; }
        public List<int> InstrumentIds { get; set; }
    }
}