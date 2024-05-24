namespace mvstwo.Models
{
    public class Quest
    {
        public int Id { get; set; }
        public string QuestText { get; set; } = "";
        public string TypeQuest { get; set; } = "";
        public List<Answer> answers { get; set; }
        public Test? test { get; set; }
    }
}
