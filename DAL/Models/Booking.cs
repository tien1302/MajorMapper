using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int ConsultantId { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public string Status { get; set; } = null!;

    public virtual Account Consultant { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Account Student { get; set; } = null!;
}
