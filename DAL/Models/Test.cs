using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Test
{
    public int Id { get; set; }

    public int PlayerId { get; set; }

    public bool StatusGame { get; set; }

    public bool StatusPayment { get; set; }

    public DateTime CreateDateTime { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Account Player { get; set; } = null!;

    public virtual ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();

    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
}
