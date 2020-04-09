using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DAL.Entities
{
    public class SheetEF
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SheetPartEF> SheetParts { get; set; }
        public bool IsCurrent { get; set; }
        public string Composer { get; set; }
        public string Arranger { get; set; }
        public CategoryEF Category { get; set; }
        public bool IsIndependance { get; set; }
        public bool IsGarde { get; set; }
    }
}
