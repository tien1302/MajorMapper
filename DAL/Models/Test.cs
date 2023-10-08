using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Test
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public bool Status { get; set; }

    public DateTime CreateDateTime { get; set; }

    public virtual ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();

    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();

    public virtual Account User { get; set; } = null!;
}
