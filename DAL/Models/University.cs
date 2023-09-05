using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class University
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int Phone { get; set; }

    public string Email { get; set; } = null!;

    public string Website { get; set; } = null!;

    public string Icon { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public DateTime UpdatedDateTime { get; set; }

    public virtual ICollection<Major> Majors { get; set; } = new List<Major>();
}
