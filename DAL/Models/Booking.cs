using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int PlayerId { get; set; }

    public int SlotId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreateDateTime { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Account Player { get; set; } = null!;

    public virtual Slot Slot { get; set; } = null!;
}
