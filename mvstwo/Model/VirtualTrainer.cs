using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class VirtualTrainer
{
    public int Id { get; set; }

    public string? DescriptionCase { get; set; }

    public int Eom { get; set; }

    public virtual Eom EomNavigation { get; set; } = null!;

    public virtual ICollection<TestBlock> TestBlocks { get; set; } = new List<TestBlock>();

    public virtual ICollection<Trainerlection> Trainerlections { get; set; } = new List<Trainerlection>();
}
