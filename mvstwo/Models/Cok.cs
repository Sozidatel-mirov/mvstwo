namespace mvstwo.Models
{
    public class Cok
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Pm { get; set; } = "";
        public string Mdk { get; set; } = "";
        public string Spo { get; set; } = "";
        public string Eom1 { get; set; } = "";
        public string Eom2 { get; set; } = "";
        public string Eom3 { get; set; } = "";
        public string Branch { get; set; } = "";
        public string RequirementsStudentLection { get; set; } = "";
        public string RequirementsStudentTrainer { get; set; } = "";
        public string RequirementsStudentTest { get; set; } = "";
        public string RequirementsTeacherLection { get; set; } = "";
        public string RequirementsTeacherTrainer { get; set; } = "";
        public string RequirementsTeacherTest { get; set; } = "";
        public User? teacher { get; set; }
        public List<keyword>? keywords { get; set; }
        public List<Lection>? lections { get; set; }
        public List<Test>? tests { get; set; }
        public List<Trainer>? trainers { get; set; }

    }
}
