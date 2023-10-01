using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Accounts
{
    public class UpdateAccount
    {
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
    }
}
