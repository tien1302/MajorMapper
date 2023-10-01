using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SlotId { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Slot Slot { get; set; } = null!;

    public virtual Account User { get; set; } = null!;
}
