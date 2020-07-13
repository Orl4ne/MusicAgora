using System.ComponentModel.DataAnnotations;

namespace Library.DAL.Entities
{
    public class SheetPartEF
    {
        public int Id { get; set; }
        //[Required]
        public InstrumentEF Instrument { get; set; }
        //[Required]
        public string Path { get; set; }
        [Required]
        public SheetEF Sheet { get; set; }
    }
}
