using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.TestQuestions
{
    public class UpdateTestQuestion
    {
        public int TestId { get; set; }

        public int QuestionId { get; set; }

        public int PersonalityTypeId { get; set; }

        public int Score { get; set; }

        public bool Status { get; set; }
    }
}
