using BAL.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Accounts
{
    public class ResetPassword
    {
       

        public int Id { get; set; }
        public string OldPassword { get; set; } = null!;
        [ResetPassword("OldPassword")]
        public string Password { get; set; } = null!;
        [Password("Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
