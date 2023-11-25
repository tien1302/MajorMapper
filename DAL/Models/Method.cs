using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Method
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreateDateTime { get; set; }

    public virtual ICollection<PersonalityType> PersonalityTypes { get; set; } = new List<PersonalityType>();
}
