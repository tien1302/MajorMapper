using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Bookings
{
    public class GetBooking
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }

        public int ConsultantId { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string Status { get; set; } = null!;
    }
}
