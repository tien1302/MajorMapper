using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.TestQuestions
{
    public class GetTestQuestion
    {
        public int TestId { get; set; }

        public int QuestionId { get; set; }

        public string GameData { get; set; }

        public bool Status { get; set; }
    }
}
