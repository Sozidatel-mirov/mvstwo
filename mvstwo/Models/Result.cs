namespace mvstwo.Models
{
    public class Result
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Cok Cok { get; set; }
        public int balls { get; set; }
        public int TimeSec { get; set; }
        public int TimeMin { get; set; }
    }
}
