using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Questions
{
    public class GetQuestion
    {
        [Key]
        public int Id { get; set; }

        public int Type { get; set; }
        public int PersonalityTypeId { get; set; }
        public string PersonalityTypeName { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
