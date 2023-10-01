using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.PersonalityTypes
{
    public class UpdatePersonalityType
    {
        [Required(ErrorMessage = "PersonalityType Name is required.")]
        [MaxLength(200, ErrorMessage = "PersonalityType Name is required less than or equals 200 characters.")]
        [RegularExpression("[a-zA-Z/ 0-9]*", ErrorMessage = "Pet Name must be included a-z, A-Z, /, space and digit 0-9.")]
        public string Name { get; set; }
    }
}
