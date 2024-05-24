using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class TestAccordBlock
{
    public int Id { get; set; }

    public string? FirstPhrase { get; set; }

    public int? FirstImage { get; set; }

    public string? SecondPhrase { get; set; }

    public int? SecondImage { get; set; }

    public int Test { get; set; }

    public virtual Image? FirstImageNavigation { get; set; }

    public virtual Image? SecondImageNavigation { get; set; }

    public virtual Test TestNavigation { get; set; } = null!;
}
