using BAL.DTOs.Scores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.TestResults
{
    public class GetTestResult
    {
        [Key]
        public int Id { get; set; }

        public int TestId { get; set; }

        public string MethodName { get; set; } = null!;

        public DateTime CreateDateTime { get; set; }

        public List<GetScore> getScores { get; set; }
    }
}
