using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class Content
{
    public int Id { get; set; }

    public int IdCoks { get; set; }

    public int IdEom1 { get; set; }

    public int IdEom2 { get; set; }

    public int IdEom3 { get; set; }

    public virtual Cok IdCoksNavigation { get; set; } = null!;

    public virtual Eom IdEom1Navigation { get; set; } = null!;

    public virtual Eom IdEom2Navigation { get; set; } = null!;

    public virtual Eom IdEom3Navigation { get; set; } = null!;
}
