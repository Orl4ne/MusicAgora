using Library.DAL.Auth;
using System.Collections.Generic;

namespace Library.DAL.Entities
{
    public class InstrumentEF
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserInstrumentEF> UserInstrument { get; set; }
    }
}