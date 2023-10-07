using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Test
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreateDateTime { get; set; }

    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();

    public virtual Account User { get; set; } = null!;
}
