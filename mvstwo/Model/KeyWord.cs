using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class KeyWord
{
    public int Id { get; set; }

    public string Word { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int IdCoks { get; set; }

    public virtual Cok IdCoksNavigation { get; set; } = null!;
}
