using Microsoft.AspNetCore.Identity;
using MusicAgora.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicAgora.Common.Library.TransferObjects
{
    public class UserTO : IdentityUser<int>
    {
        public AccessRight AccessRight { get; set; }
        public List<InstrumentTO> Instruments { get; set; }
        public bool IsIndependance { get; set; }
        public bool IsGarde { get; set; }
    }
}
