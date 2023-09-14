using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Majors
{
    public class CreateMajor
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public List<int> PersonalityTypeId { get; set; }
    }
}
