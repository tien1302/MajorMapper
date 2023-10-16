using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Scores
{
    public class CreateScore
    {
        public int TestResultId { get; set; }

        public int PersonalityTypeId { get; set; }

        public int Result { get; set; }
    }
}
