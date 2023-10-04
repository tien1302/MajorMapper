using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Majors
{
    public class CreateMajor
    {
        [Required(ErrorMessage = "Major Name is required.")]
        [MaxLength(200, ErrorMessage = "Major Name is required less than or equals 200 characters.")]
        [RegularExpression("[a-zA-Z/ 0-9]*", ErrorMessage = "Major Name must be included a-z, A-Z, /, space and digit 0-9.")]
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public List<int> PersonalityTypeId { get; set; }
    }
}
