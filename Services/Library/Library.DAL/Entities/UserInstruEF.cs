using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DAL.Entities
{
    public class UserInstruEF
    {
        public int LibUserId { get; set; }
        public LibUserEF LibUser { get; set; }
        public int InstrumentId { get; set; }
        public InstrumentEF Instrument { get; set; }
    }
}
