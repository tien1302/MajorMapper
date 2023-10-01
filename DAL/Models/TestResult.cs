using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class TestResult
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<ReviewTest> ReviewTests { get; set; } = new List<ReviewTest>();

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
