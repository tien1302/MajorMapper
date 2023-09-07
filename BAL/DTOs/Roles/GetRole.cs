using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Roles
{
    public class GetRole
    {
        [Key]
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
    }
}
