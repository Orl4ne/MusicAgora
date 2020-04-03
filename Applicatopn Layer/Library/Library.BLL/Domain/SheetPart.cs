namespace Library.BLL.Domain
{
    public class SheetPart
    {
        public int Id { get; set; }
        public Instrument Instrument { get; set; }
        public string Path { get; set; }
        public Sheet Sheet { get; set; }
    }
}
