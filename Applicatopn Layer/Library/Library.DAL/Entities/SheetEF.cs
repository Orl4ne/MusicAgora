using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.DAL.Entities
{
    public class SheetEF
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsCurrent { get; set; }
        public string Composer { get; set; }
        public string Arranger { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public bool IsIndependance { get; set; }
        [Required]
        public bool IsGarde { get; set; }
    }
}
