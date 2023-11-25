using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class TestResult
{
    public int Id { get; set; }

    public int TestId { get; set; }

    public string MethodName { get; set; } = null!;

    public DateTime CreateDateTime { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();

    public virtual Test Test { get; set; } = null!;
}
