using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class User
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Surename { get; set; } = null!;

    public int RoleUser { get; set; }

    public int? Usergroup { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Cok> Coks { get; set; } = new List<Cok>();

    public virtual ICollection<ResultsTest> ResultsTests { get; set; } = new List<ResultsTest>();

    public virtual Role RoleUserNavigation { get; set; } = null!;

    public virtual Group? UsergroupNavigation { get; set; }
}
