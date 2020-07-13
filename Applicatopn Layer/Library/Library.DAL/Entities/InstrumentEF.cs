using System.Collections.Generic;

namespace Library.DAL.Entities
{
    public class InstrumentEF
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> UserIds { get; set; }
    }
}