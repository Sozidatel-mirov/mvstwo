using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class Video
{
    public int Id { get; set; }

    public string Path { get; set; } = null!;

    public virtual ICollection<LectionBlock> LectionBlocks { get; set; } = new List<LectionBlock>();

    public virtual ICollection<TestBlock> TestBlocks { get; set; } = new List<TestBlock>();

    public virtual ICollection<Trainerlection> Trainerlections { get; set; } = new List<Trainerlection>();
}
