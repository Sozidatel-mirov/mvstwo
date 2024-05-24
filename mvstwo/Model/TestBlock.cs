using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class TestBlock
{
    public int Id { get; set; }

    public string? Textlection { get; set; }

    public int? Image { get; set; }

    public int? Video { get; set; }

    public int VirtualTrainer { get; set; }

    public int Test { get; set; }

    public virtual Image? ImageNavigation { get; set; }

    public virtual Test TestNavigation { get; set; } = null!;

    public virtual Video? VideoNavigation { get; set; }

    public virtual VirtualTrainer VirtualTrainerNavigation { get; set; } = null!;
}
