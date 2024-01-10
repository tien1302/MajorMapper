using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Questions
{
    public class UpdateQuestion
    {
        [Required(ErrorMessage = "Không được để trống")]
        public string Status { get; set; }
    }
}
