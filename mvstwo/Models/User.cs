
namespace mvstwo.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; } // имя пользователя
        public string? Sname { get; set; } // фамилия пользователя
        public string? Role {get; set; } // роль на сайте = студент\преподаватель\администратор
        public int groupId { get; set; }
        public Group? group { get; set; }
        public List<Cok>? cok { get; set; } // список созданных цоков(для преподавателя)
        public bool KeepLoggedIn { get; set; }
    }
}
