using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class TestSequenceBlock
{
    public int Id { get; set; }

    public string? Phrase { get; set; }

    public int? Image { get; set; }

    public int Number { get; set; }

    public int Test { get; set; }

    public virtual Image? ImageNavigation { get; set; }

    public virtual Test TestNavigation { get; set; } = null!;
}
