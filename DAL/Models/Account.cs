using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Account
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Gender { get; set; }

    public DateTime? DoB { get; set; }

    public int Role { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreateDateTime { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Role RoleNavigation { get; set; } = null!;

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
