using BAL.DTOs.Bookings;
using BAL.DTOs.Notifications;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface INotificationService
    {
        public List<GetNotification> GetAll();
        public GetNotification Get(int key);
        public void Create(int? bookingId);
        public List<GetNotification> GetAllByConsultantId(string key);
    }
}
