﻿using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Major
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public DateTime UpdatedDateTime { get; set; }

    public virtual ICollection<PersonalityType> PersonalityTypes { get; set; } = new List<PersonalityType>();

    public virtual ICollection<University> Universities { get; set; } = new List<University>();
}
