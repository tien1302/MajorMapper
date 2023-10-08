using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class PersonalityType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreateDateTime { get; set; }

    public DateTime UpdateDateTime { get; set; }

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();

    public virtual ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();

    public virtual ICollection<Major> Majors { get; set; } = new List<Major>();
}
