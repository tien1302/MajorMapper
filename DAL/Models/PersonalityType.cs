using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class PersonalityType
{
    public int Id { get; set; }

    public int MethodId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreateDateTime { get; set; }

    public DateTime UpdateDateTime { get; set; }

    public virtual Method Method { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();

    public virtual ICollection<Major> Majors { get; set; } = new List<Major>();
}
