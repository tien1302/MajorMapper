using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Feedbacks
{
    public class UpdateFeedback
    {
        public int BookingId { get; set; }

        public string? Comment { get; set; }

        public int Star { get; set; }
    }
}
