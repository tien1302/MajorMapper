using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.TestResults
{
    public class CreateTestResult
    {
        public int TestId { get; set; }

        public string MethodName { get; set; } = null!;
    }
}
