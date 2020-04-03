using System;
using System.Collections.Generic;
using System.Text;

namespace MusicAgora.Common.Library.TransferObjects
{
    public class SheetPartTO
    {
        public int Id { get; set; }
        public Instrument Instrument { get; set; }
        public string Path { get; set; }
        public Sheet Sheet { get; set; }
    }
}
