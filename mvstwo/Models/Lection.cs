namespace mvstwo.Models
{
    public class Lection
    {
        public int Id { get; set; }
        public string TextBlock { get; set; } = "";
        public string Img { get; set; } = "";
        public Cok? cok { get; set; }
        public Trainer? trainer { get; set; }
    }
}
