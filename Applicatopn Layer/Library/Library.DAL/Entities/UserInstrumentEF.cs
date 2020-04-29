using Library.DAL.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DAL.Entities
{
    public class UserInstrumentEF
    {
        public int UserID { get; set; }
        public User User { get; set; }

        public int InstrumentId { get; set; }
        public InstrumentEF Instrument { get; set; }
    }
}