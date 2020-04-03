using System;
using System.Collections.Generic;
using System.Text;

namespace MusicAgora.Common.Library.TransferObjects
{
    public class SheetTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SheetPart> SheetParts { get; set; }
        public bool IsCurrent { get; set; }
        public string Composer { get; set; }
        public string Arranger { get; set; }
        public Category Category { get; set; }
        public bool IsIndependance { get; set; }
        public bool IsGarde { get; set; }
    }
}
