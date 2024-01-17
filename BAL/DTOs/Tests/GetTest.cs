using BAL.DTOs.Scores;
using BAL.DTOs.TestResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Tests
{
    public class GetTest
    {
        [Key]
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public bool StatusGame { get; set; }

        public bool StatusPayment { get; set; }

        public List<GetTestResult> getTestResults { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
