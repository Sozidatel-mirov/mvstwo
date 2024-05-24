using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class Test
{
    public int Id { get; set; }

    public virtual ICollection<Quest> Quests { get; set; } = new List<Quest>();

    public virtual ICollection<TestAccordBlock> TestAccordBlocks { get; set; } = new List<TestAccordBlock>();

    public virtual ICollection<TestBlock> TestBlocks { get; set; } = new List<TestBlock>();

    public virtual ICollection<TestSequenceBlock> TestSequenceBlocks { get; set; } = new List<TestSequenceBlock>();
}
