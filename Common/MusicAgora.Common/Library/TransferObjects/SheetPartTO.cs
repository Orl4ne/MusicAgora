using System;
using System.Collections.Generic;
using System.Text;

namespace MusicAgora.Common.Library.TransferObjects
{
    public class SheetPartTO
    {
        public int Id { get; set; }
        public int InstrumentId { get; set; }
        public string Path { get; set; }
        public int SheetId { get; set; }
    }
}
