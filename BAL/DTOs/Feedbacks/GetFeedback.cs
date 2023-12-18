using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Feedbacks
{
    public class GetFeedback
    {
        [Key]
        public int Id { get; set; }

        public int BookingId { get; set; }
        public string Name { get; set; }

        public string? Comment { get; set; }

        public int Star { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
