﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.TestQuestions
{
    public class CreateTestQuestion
    {
        public int TestId { get; set; }

        public int QuestionId { get; set; }
        public bool Status { get; set; }
    }
}
