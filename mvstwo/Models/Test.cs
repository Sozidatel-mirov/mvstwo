namespace mvstwo.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<Quest>? Questions { get; set; }
        public Cok? cok { get; set; }
        public Trainer? trainer { get; set; }
    }
}
