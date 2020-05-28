using System.Collections.Generic;

namespace MusicAgora.Common.Library.TransferObjects
{
    public class InstrumentTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> UserIds { get; set; }
    }
}