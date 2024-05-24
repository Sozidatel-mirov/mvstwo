using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class Eom
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Content> ContentIdEom1Navigations { get; set; } = new List<Content>();

    public virtual ICollection<Content> ContentIdEom2Navigations { get; set; } = new List<Content>();

    public virtual ICollection<Content> ContentIdEom3Navigations { get; set; } = new List<Content>();

    public virtual ICollection<Lection> Lections { get; set; } = new List<Lection>();

    public virtual ICollection<ResultsTest> ResultsTests { get; set; } = new List<ResultsTest>();

    public virtual ICollection<VirtualTrainer> VirtualTrainers { get; set; } = new List<VirtualTrainer>();
}
