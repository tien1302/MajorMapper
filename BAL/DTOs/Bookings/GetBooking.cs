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

        public int UserId { get; set; }

        public int SlotId { get; set; }

        public string Status { get; set; } = null!;
    }
}
