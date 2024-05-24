namespace mvstwo.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public int page { get; set; }
        public Cok? cok { get; set; }
        public List<Lection>? lection { get; set; }
        public List<Test>? test { get; set; }
    }
}
