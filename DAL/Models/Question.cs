using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Question
{
    public int Id { get; set; }

    public int PersonalityTypeId { get; set; }


    public string Description { get; set; } = null!;

    public DateTime CreateDateTime { get; set; }

    public string Status { get; set; } = null!;

    public virtual PersonalityType PersonalityType { get; set; } = null!;

    public virtual ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();
}
