namespace mvstwo.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string AnswerText { get; set; } = "";
        public bool Currently { get; set; }
        public Quest quest { get; set; }
    }
}
