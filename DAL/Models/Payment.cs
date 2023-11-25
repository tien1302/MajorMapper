using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string OrderType { get; set; } = null!;

    public int RelatiedId { get; set; }

    public string OrderId { get; set; } = null!;

    public string TransactionId { get; set; } = null!;

    public int Amount { get; set; }

    public string Description { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreateDateTime { get; set; }

    public virtual Booking Relatied { get; set; } = null!;

    public virtual TestResult RelatiedNavigation { get; set; } = null!;

    public virtual Account User { get; set; } = null!;
}
