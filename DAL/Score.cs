using System;
using System.Collections.Generic;

namespace DAL;

public partial class Score
{
    public int TestResultId { get; set; }

    public int PersonalityTypeId { get; set; }

    public int Result { get; set; }

    public virtual PersonalityType PersonalityType { get; set; } = null!;

    public virtual TestResult TestResult { get; set; } = null!;
}
