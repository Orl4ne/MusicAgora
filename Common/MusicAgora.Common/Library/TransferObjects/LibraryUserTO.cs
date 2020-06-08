using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicAgora.Common.Library.TransferObjects
{
    public class LibraryUserTO
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public LibraryAccessTO AccessRight { get; set; }
        public List<int> InstrumentIds { get; set; }
        public bool IsIndependance { get; set; }
        public bool IsGarde { get; set; }
    }
}
