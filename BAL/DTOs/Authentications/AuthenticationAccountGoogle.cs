using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Authentications
{
    public class AuthenticationAccountGoogle
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
    }
}
