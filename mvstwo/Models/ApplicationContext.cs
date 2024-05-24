using Microsoft.EntityFrameworkCore;
namespace mvstwo.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<keyword> Keywords { get; set; } = null!;
        public DbSet<Lection> Lections { get; set; } = null!;
        public DbSet<Quest> Quests { get; set; } = null!;
        public DbSet<Test> Tests { get; set; } = null!;
        public DbSet<Trainer> Trainers { get; set; } = null!;
        public DbSet<Cok> Coks { get; set; } = null!;
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Result> Results { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            // создаем базу данных при первом обращении
        }
    }
}
