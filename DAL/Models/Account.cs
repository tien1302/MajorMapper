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

    public int? Phone { get; set; }

    public string Status { get; set; } = null!;

    public int? Turn { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public virtual ICollection<Booking> BookingConsultants { get; set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingStudents { get; set; } = new List<Booking>();

    public virtual Role RoleNavigation { get; set; } = null!;

    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
}
