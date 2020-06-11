using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DAL.Entities
{
    public class LibraryAccessRightEF 
    {
        public int Id { get; set; }
        public string Accessor { get; set; }
    }
}

