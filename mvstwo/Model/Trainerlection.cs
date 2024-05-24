using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class Trainerlection
{
    public int Id { get; set; }

    public string? Textlection { get; set; }

    public int? Image { get; set; }

    public int? Video { get; set; }

    public int VirtualTrainer { get; set; }

    public virtual Image? ImageNavigation { get; set; }

    public virtual Video? VideoNavigation { get; set; }

    public virtual VirtualTrainer VirtualTrainerNavigation { get; set; } = null!;
}
