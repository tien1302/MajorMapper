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
        [Key]
        public int Id { get; set; }

        public int TestId { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
