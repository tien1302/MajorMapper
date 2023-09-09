using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Accounts
{
    public class GetAccount
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Gender { get; set; }

        public DateTime? DoB { get; set; }

        public string RoleName { get; set; }

        public string? Address { get; set; }

        public int? Phone { get; set; }

        public string Status { get; set; } = null!;

        public int? Turn { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string AccessToken { get; set; }
    }
}
