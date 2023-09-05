using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class ReviewTest
{
    public int Id { get; set; }

    public int Star { get; set; }

    public string? Comment { get; set; }

    public int TestResultId { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public virtual TestResult TestResult { get; set; } = null!;
}
