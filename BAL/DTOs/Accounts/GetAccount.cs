using BAL.DTOs.TestResults;
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
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DoB { get; set; }

        public string RoleName { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreateDateTime { get; set; }

        public string AccessToken { get; set; }
    }
}
