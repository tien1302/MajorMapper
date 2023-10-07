using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Question
{
    public int Id { get; set; }

    public string AssetsName { get; set; } = null!;

    public int Type { get; set; }

    public DateTime CreateDateTime { get; set; }
}
