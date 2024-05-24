using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class ResultsTest
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public int QuantityCorrectAnswer { get; set; }

    public int QuantityUnCorrectAnswer { get; set; }

    public int Estimation { get; set; }

    public TimeSpan? TimePeform { get; set; }

    public int Eom { get; set; }

    public virtual Eom EomNavigation { get; set; } = null!;

    public virtual ICollection<FreeAnswer> FreeAnswers { get; set; } = new List<FreeAnswer>();

    public virtual User? IdUserNavigation { get; set; }
}
