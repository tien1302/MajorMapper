using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.PersonalityTypes
{
    public class CreatePersonalityType
    {
        [Required(ErrorMessage = "PersonalityType Name is required.")]
        [MaxLength(200, ErrorMessage = "PersonalityType Name is required less than or equals 200 characters.")]
        public string Name { get; set; }
    }
}
