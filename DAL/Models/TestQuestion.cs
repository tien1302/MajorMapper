using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class TestQuestion
{
    public int TestId { get; set; }

    public int QuestionId { get; set; }

    public int PersonalityTypeId { get; set; }

    public int Score { get; set; }

    public bool Status { get; set; }

    public virtual PersonalityType PersonalityType { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;

    public virtual Test Test { get; set; } = null!;
}
