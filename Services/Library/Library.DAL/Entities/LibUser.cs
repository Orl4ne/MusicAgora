﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DAL.Entities
{
    public class LibUser
    {
        public int LibUserId { get; set; }
        public int IdentityUserId { get; set; }
        public List<UserInstruEF> UserInstruments { get; set; }
    }
}
