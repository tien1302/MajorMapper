﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Notifications
{
    public class CreateNotification
    {
        public int? BookingId { get; set; }

        public string NotificationContent { get; set; } = null!;

        public string Title { get; set; } = null!;

        public DateTime Time { get; set; }
    }
}
