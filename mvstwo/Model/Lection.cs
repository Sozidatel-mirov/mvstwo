using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class Lection
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int? Icon { get; set; }

    public int Eom { get; set; }

    public virtual Eom EomNavigation { get; set; } = null!;

    public virtual Image IconNavigation { get; set; } = null!;

    public virtual ICollection<LectionBlock> LectionBlocks { get; set; } = new List<LectionBlock>();
}
