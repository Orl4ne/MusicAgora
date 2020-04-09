namespace Library.DAL.Entities
{
    public class SheetPartEF
    {
        public int Id { get; set; }
        public InstrumentEF Instrument { get; set; }
        public string Path { get; set; }
        public SheetEF Sheet { get; set; }
    }
}
