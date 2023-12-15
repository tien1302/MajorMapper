using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Bookings
{
    public class CreateBooking
    {
        public int PlayerId { get; set; }

        public int SlotId { get; set; }
    }
}
