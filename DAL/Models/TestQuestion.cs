using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class TestQuestion
{
    public int TestId { get; set; }

    public int QuestionId { get; set; }

    public string? GameData { get; set; }

    public bool Status { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual Test Test { get; set; } = null!;
}
