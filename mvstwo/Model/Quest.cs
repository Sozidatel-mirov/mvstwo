using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class Quest
{
    public int Id { get; set; }

    public string? TextQuest { get; set; }

    public int? Image { get; set; }

    public int Test { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<FreeAnswer> FreeAnswers { get; set; } = new List<FreeAnswer>();

    public virtual Image? ImageNavigation { get; set; }

    public virtual Test TestNavigation { get; set; } = null!;
}
