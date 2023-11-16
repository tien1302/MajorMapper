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
        public int id { get; set; }
        public string name { get; set; } = null!;

        public string email { get; set; } = null!;

        public string password { get; set; } = null!;

        public string? gender { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? doB { get; set; }

        public string roleName { get; set; }

        public string? address { get; set; }

        public string? phone { get; set; }

        public string status { get; set; } = null!;

        public DateTime createDateTime { get; set; }

        public string accessToken { get; set; }
    }
}
