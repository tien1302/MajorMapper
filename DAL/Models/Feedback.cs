using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public string? Comment { get; set; }

    public int Star { get; set; }

    public DateTime CreateDateTime { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
