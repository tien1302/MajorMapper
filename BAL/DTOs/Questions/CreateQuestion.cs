using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Questions
{
    public class CreateQuestion
    {
        public int PersonalityTypeId { get; set; }

        public string Description { get; set; }
    }
}
