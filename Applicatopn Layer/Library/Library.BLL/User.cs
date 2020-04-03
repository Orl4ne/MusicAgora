using Microsoft.AspNetCore.Identity;
using MusicAgora.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BLL
{
    public class User : IdentityUser<int>
    {
        public AccessRight AccessRight { get; set; }
        public List<Instrument> Instruments { get; set; }
        public bool IsIndependance { get; set; }
        public bool IsGarde { get; set; }
    }
}
