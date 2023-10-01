using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Notifications
{
    public class UpdateNotification
    {
        public int? BookingId { get; set; }

        public int AccountId { get; set; }

        public string NotificationContent { get; set; } = null!;

        public string Title { get; set; } = null!;

        public DateTime Time { get; set; }

        public bool IsRead { get; set; }
    }
}
