using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace mvstwo.Model;

public partial class OkeiSiteContext : DbContext
{
    public OkeiSiteContext()
    {
    }

    public OkeiSiteContext(DbContextOptions<OkeiSiteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Cok> Coks { get; set; }

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<Eom> Eoms { get; set; }

    public virtual DbSet<FreeAnswer> FreeAnswers { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<KeyWord> KeyWords { get; set; }

    public virtual DbSet<Lection> Lections { get; set; }

    public virtual DbSet<LectionBlock> LectionBlocks { get; set; }

    public virtual DbSet<Quest> Quests { get; set; }

    public virtual DbSet<ResultsTest> ResultsTests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<TestAccordBlock> TestAccordBlocks { get; set; }

    public virtual DbSet<TestBlock> TestBlocks { get; set; }

    public virtual DbSet<TestSequenceBlock> TestSequenceBlocks { get; set; }

    public virtual DbSet<Trainerlection> Trainerlections { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    public virtual DbSet<VirtualTrainer> VirtualTrainers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=WIN-B8E2CK3R5BJ;Initial Catalog=okeiSite;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Answers__3213E83F4D681B91");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsCorrect).HasColumnName("isCorrect");
            entity.Property(e => e.TextAnswers)
                .HasColumnType("text")
                .HasColumnName("textAnswers");

            entity.HasOne(d => d.ImageNavigation).WithMany(p => p.Answers)
                .HasForeignKey(d => d.Image)
                .HasConstraintName("FK__Answers__Image__6D0D32F4");

            entity.HasOne(d => d.QuestNavigation).WithMany(p => p.Answers)
                .HasForeignKey(d => d.Quest)
                .HasConstraintName("FK__Answers__Quest__6C190EBB");
        });

        modelBuilder.Entity<Cok>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coks__3213E83FC114849B");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdTeacher).HasColumnName("idTeacher");
            entity.Property(e => e.Mdk)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MDK");
            entity.Property(e => e.Pm)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PM");
            entity.Property(e => e.Spo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SPO");
            entity.Property(e => e.Theme)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTeacherNavigation).WithMany(p => p.Coks)
                .HasForeignKey(d => d.IdTeacher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Coks__idTeacher__3F466844");
        });

        modelBuilder.Entity<Content>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Content__3213E83FE512137C");

            entity.ToTable("Content");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCoks).HasColumnName("idCoks");
            entity.Property(e => e.IdEom1).HasColumnName("idEOM1");
            entity.Property(e => e.IdEom2).HasColumnName("idEOM2");
            entity.Property(e => e.IdEom3).HasColumnName("idEOM3");

            entity.HasOne(d => d.IdCoksNavigation).WithMany(p => p.Contents)
                .HasForeignKey(d => d.IdCoks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Content__idCoks__46E78A0C");

            entity.HasOne(d => d.IdEom1Navigation).WithMany(p => p.ContentIdEom1Navigations)
                .HasForeignKey(d => d.IdEom1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Content__idEOM1__47DBAE45");

            entity.HasOne(d => d.IdEom2Navigation).WithMany(p => p.ContentIdEom2Navigations)
                .HasForeignKey(d => d.IdEom2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Content__idEOM2__48CFD27E");

            entity.HasOne(d => d.IdEom3Navigation).WithMany(p => p.ContentIdEom3Navigations)
                .HasForeignKey(d => d.IdEom3)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Content__idEOM3__49C3F6B7");
        });

        modelBuilder.Entity<Eom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EOM__3213E83F7730F09B");

            entity.ToTable("EOM");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FreeAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FreeAnsw__3213E83FAFDBDDBA");

            entity.ToTable("FreeAnswer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Answer).HasColumnType("text");
            entity.Property(e => e.IdQuest).HasColumnName("idQuest");
            entity.Property(e => e.IdResults).HasColumnName("idResults");

            entity.HasOne(d => d.IdQuestNavigation).WithMany(p => p.FreeAnswers)
                .HasForeignKey(d => d.IdQuest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FreeAnswe__idQue__7D439ABD");

            entity.HasOne(d => d.IdResultsNavigation).WithMany(p => p.FreeAnswers)
                .HasForeignKey(d => d.IdResults)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FreeAnswe__idRes__7C4F7684");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Groups__3213E83F51292913");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Images__3213E83F239609D5");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Path).HasColumnType("text");
        });

        modelBuilder.Entity<KeyWord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KeyWords__3213E83FA321F4AF");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IdCoks).HasColumnName("idCoks");
            entity.Property(e => e.Word)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCoksNavigation).WithMany(p => p.KeyWords)
                .HasForeignKey(d => d.IdCoks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KeyWords__idCoks__4222D4EF");
        });

        modelBuilder.Entity<Lection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Lection__3213E83F38C8FCFC");

            entity.ToTable("Lection");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Eom).HasColumnName("eom");
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.EomNavigation).WithMany(p => p.Lections)
                .HasForeignKey(d => d.Eom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Lection__eom__5165187F");

            entity.HasOne(d => d.IconNavigation).WithMany(p => p.Lections)
                .HasForeignKey(d => d.Icon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Lection__icon__5070F446");
        });

        modelBuilder.Entity<LectionBlock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LectionB__3213E83F6D724FF2");

            entity.ToTable("LectionBlock");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdLection).HasColumnName("idLection");
            entity.Property(e => e.Textlection)
                .HasColumnType("text")
                .HasColumnName("textlection");

            entity.HasOne(d => d.IdLectionNavigation).WithMany(p => p.LectionBlocks)
                .HasForeignKey(d => d.IdLection)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LectionBl__idLec__5441852A");

            entity.HasOne(d => d.ImageNavigation).WithMany(p => p.LectionBlocks)
                .HasForeignKey(d => d.Image)
                .HasConstraintName("FK__LectionBl__Image__5535A963");

            entity.HasOne(d => d.VideoNavigation).WithMany(p => p.LectionBlocks)
                .HasForeignKey(d => d.Video)
                .HasConstraintName("FK__LectionBl__Video__5629CD9C");
        });

        modelBuilder.Entity<Quest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Quest__3213E83F21C0C0DE");

            entity.ToTable("Quest");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TextQuest)
                .HasColumnType("text")
                .HasColumnName("textQuest");

            entity.HasOne(d => d.ImageNavigation).WithMany(p => p.Quests)
                .HasForeignKey(d => d.Image)
                .HasConstraintName("FK__Quest__Image__68487DD7");

            entity.HasOne(d => d.TestNavigation).WithMany(p => p.Quests)
                .HasForeignKey(d => d.Test)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Quest__Test__693CA210");
        });

        modelBuilder.Entity<ResultsTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ResultsT__3213E83FCC46D9D8");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Eom).HasColumnName("eom");
            entity.Property(e => e.IdUser).HasColumnName("idUser");

            entity.HasOne(d => d.EomNavigation).WithMany(p => p.ResultsTests)
                .HasForeignKey(d => d.Eom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ResultsTest__eom__797309D9");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.ResultsTests)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__ResultsTe__idUse__787EE5A0");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3213E83F4E18E09C");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Test__3213E83F12C0B495");

            entity.ToTable("Test");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<TestAccordBlock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TestAcco__3213E83F5F57B785");

            entity.ToTable("TestAccordBlock");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstImage).HasColumnName("firstImage");
            entity.Property(e => e.FirstPhrase)
                .HasColumnType("text")
                .HasColumnName("firstPhrase");
            entity.Property(e => e.SecondPhrase).HasColumnType("text");

            entity.HasOne(d => d.FirstImageNavigation).WithMany(p => p.TestAccordBlockFirstImageNavigations)
                .HasForeignKey(d => d.FirstImage)
                .HasConstraintName("FK__TestAccor__first__6FE99F9F");

            entity.HasOne(d => d.SecondImageNavigation).WithMany(p => p.TestAccordBlockSecondImageNavigations)
                .HasForeignKey(d => d.SecondImage)
                .HasConstraintName("FK__TestAccor__Secon__70DDC3D8");

            entity.HasOne(d => d.TestNavigation).WithMany(p => p.TestAccordBlocks)
                .HasForeignKey(d => d.Test)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TestAccord__Test__71D1E811");
        });

        modelBuilder.Entity<TestBlock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TestBloc__3213E83FDADDCA78");

            entity.ToTable("TestBlock");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Textlection)
                .HasColumnType("text")
                .HasColumnName("textlection");

            entity.HasOne(d => d.ImageNavigation).WithMany(p => p.TestBlocks)
                .HasForeignKey(d => d.Image)
                .HasConstraintName("FK__TestBlock__Image__628FA481");

            entity.HasOne(d => d.TestNavigation).WithMany(p => p.TestBlocks)
                .HasForeignKey(d => d.Test)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TestBlock__Test__656C112C");

            entity.HasOne(d => d.VideoNavigation).WithMany(p => p.TestBlocks)
                .HasForeignKey(d => d.Video)
                .HasConstraintName("FK__TestBlock__Video__6383C8BA");

            entity.HasOne(d => d.VirtualTrainerNavigation).WithMany(p => p.TestBlocks)
                .HasForeignKey(d => d.VirtualTrainer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TestBlock__Virtu__6477ECF3");
        });

        modelBuilder.Entity<TestSequenceBlock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TestSequ__3213E83FC6F5D876");

            entity.ToTable("TestSequenceBlock");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Phrase).HasColumnType("text");

            entity.HasOne(d => d.ImageNavigation).WithMany(p => p.TestSequenceBlocks)
                .HasForeignKey(d => d.Image)
                .HasConstraintName("FK__TestSeque__Image__74AE54BC");

            entity.HasOne(d => d.TestNavigation).WithMany(p => p.TestSequenceBlocks)
                .HasForeignKey(d => d.Test)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TestSequen__Test__75A278F5");
        });

        modelBuilder.Entity<Trainerlection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Trainerl__3213E83FE2ABAE24");

            entity.ToTable("Trainerlection");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Textlection)
                .HasColumnType("text")
                .HasColumnName("textlection");

            entity.HasOne(d => d.ImageNavigation).WithMany(p => p.Trainerlections)
                .HasForeignKey(d => d.Image)
                .HasConstraintName("FK__Trainerle__Image__5BE2A6F2");

            entity.HasOne(d => d.VideoNavigation).WithMany(p => p.Trainerlections)
                .HasForeignKey(d => d.Video)
                .HasConstraintName("FK__Trainerle__Video__5CD6CB2B");

            entity.HasOne(d => d.VirtualTrainerNavigation).WithMany(p => p.Trainerlections)
                .HasForeignKey(d => d.VirtualTrainer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trainerle__Virtu__5DCAEF64");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83FA73B7049");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Firstname)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Login)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Surename)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Usergroup).HasColumnName("usergroup");

            entity.HasOne(d => d.RoleUserNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleUser__3B75D760");

            entity.HasOne(d => d.UsergroupNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Usergroup)
                .HasConstraintName("FK__Users__usergroup__3C69FB99");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Video__3213E83F3D739B2E");

            entity.ToTable("Video");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Path).HasColumnType("text");
        });

        modelBuilder.Entity<VirtualTrainer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VirtualT__3213E83FA33425F6");

            entity.ToTable("VirtualTrainer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DescriptionCase).HasColumnType("text");
            entity.Property(e => e.Eom).HasColumnName("eom");

            entity.HasOne(d => d.EomNavigation).WithMany(p => p.VirtualTrainers)
                .HasForeignKey(d => d.Eom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VirtualTrai__eom__59063A47");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
