using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int? BookingId { get; set; }

    public string NotificationContent { get; set; } = null!;

    public string Title { get; set; } = null!;

    public DateTime Time { get; set; }

    public virtual Booking? Booking { get; set; }
}
