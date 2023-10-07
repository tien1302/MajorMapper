using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Slot
{
    public int Id { get; set; }

    public int ConsultantId { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreateDateTime { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Account Consultant { get; set; } = null!;
}
