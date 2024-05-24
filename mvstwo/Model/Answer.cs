using System;
using System.Collections.Generic;

namespace mvstwo.Model;

public partial class Answer
{
    public int Id { get; set; }

    public int? Quest { get; set; }

    public string? TextAnswers { get; set; }

    public int? Image { get; set; }

    public bool IsCorrect { get; set; }

    public virtual Image? ImageNavigation { get; set; }

    public virtual Quest? QuestNavigation { get; set; }
}
