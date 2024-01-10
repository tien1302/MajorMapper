using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Questions
{
    public class CreateQuestion
    {
        [Required(ErrorMessage = "Không được để trống")]
        public int PersonalityTypeId { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string Description { get; set; }
    }
}
