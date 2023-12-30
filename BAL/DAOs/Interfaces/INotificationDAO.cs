using BAL.DTOs.Bookings;
using BAL.DTOs.Notifications;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface INotificationDAO
    {
        public List<GetNotification> GetAll();
        public GetNotification Get(int key);
        public void Create(CreateNotification create);
        public void Update(int key, UpdateNotification update);
        public void Delete(int key);
        public List<GetNotification> GetAllByConsultantId(string key);
    }
}
