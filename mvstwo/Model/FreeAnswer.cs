using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class FreeAnswer
{
    public int Id { get; set; }

    public int IdResults { get; set; }

    public int IdQuest { get; set; }

    public string Answer { get; set; } = null!;

    public virtual Quest IdQuestNavigation { get; set; } = null!;

    public virtual ResultsTest IdResultsNavigation { get; set; } = null!;
}
