using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class Image
{
    public int Id { get; set; }

    public string Path { get; set; } = null!;

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<LectionBlock> LectionBlocks { get; set; } = new List<LectionBlock>();

    public virtual ICollection<Lection> Lections { get; set; } = new List<Lection>();

    public virtual ICollection<Quest> Quests { get; set; } = new List<Quest>();

    public virtual ICollection<TestAccordBlock> TestAccordBlockFirstImageNavigations { get; set; } = new List<TestAccordBlock>();

    public virtual ICollection<TestAccordBlock> TestAccordBlockSecondImageNavigations { get; set; } = new List<TestAccordBlock>();

    public virtual ICollection<TestBlock> TestBlocks { get; set; } = new List<TestBlock>();

    public virtual ICollection<TestSequenceBlock> TestSequenceBlocks { get; set; } = new List<TestSequenceBlock>();

    public virtual ICollection<Trainerlection> Trainerlections { get; set; } = new List<Trainerlection>();
}
