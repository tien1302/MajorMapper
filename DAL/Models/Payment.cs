using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string OrderType { get; set; } = null!;

    public int? BookingId { get; set; }

    public int? TestResultId { get; set; }

    public string OrderId { get; set; } = null!;

    public string TransactionId { get; set; } = null!;

    public int Amount { get; set; }

    public string Description { get; set; } = null!;

    public DateTime CreateDateTime { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual TestResult? TestResult { get; set; }

    public virtual Account User { get; set; } = null!;
}
