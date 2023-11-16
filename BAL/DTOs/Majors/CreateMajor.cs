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

        [MaxLength(100, ErrorMessage = "Tên không vượt quá 100 ký tự.")]
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public List<int> PersonalityTypeId { get; set; }
    }
}
