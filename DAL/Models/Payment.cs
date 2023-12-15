using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int PlayerId { get; set; }

    public int? BookingId { get; set; }

    public int? TestId { get; set; }

    public string OrderId { get; set; } = null!;

    public string TransactionId { get; set; } = null!;

    public int Amount { get; set; }

    public string Description { get; set; } = null!;

    public DateTime CreateDateTime { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Account Player { get; set; } = null!;

    public virtual Test? Test { get; set; }
}
