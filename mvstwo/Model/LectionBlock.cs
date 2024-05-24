using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class LectionBlock
{
    public int Id { get; set; }

    public int IdLection { get; set; }

    public string? Textlection { get; set; }

    public int? Image { get; set; }

    public int? Video { get; set; }

    public virtual Lection IdLectionNavigation { get; set; } = null!;

    public virtual Image? ImageNavigation { get; set; }

    public virtual Video? VideoNavigation { get; set; }
}
