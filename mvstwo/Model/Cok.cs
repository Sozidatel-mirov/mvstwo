using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class Cok
{
    public int Id { get; set; }

    public string Theme { get; set; } = null!;

    public string Mdk { get; set; } = null!;

    public string Spo { get; set; } = null!;

    public string Pm { get; set; } = null!;

    public int IdTeacher { get; set; }

    public virtual ICollection<Content> Contents { get; set; } = new List<Content>();

    public virtual User IdTeacherNavigation { get; set; } = null!;

    public virtual ICollection<KeyWord> KeyWords { get; set; } = new List<KeyWord>();
}
