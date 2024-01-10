using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Majors
{
    public class UpdateMajor
    {
        [Required(ErrorMessage = "Không được để trống")]
        [MaxLength(200, ErrorMessage = "PersonalityType Name is required less than or equals 200 characters.")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Không được để trống")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "Không được để trống")]
        public List<int> PersonalityTypeId { get; set; }
    }
}
