using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Notifications;
using DAL.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Implementations
{
    public class NotificationDAO : INotificationDAO
    {
        private NotificationRepository _Repo;
        private IMapper _mapper;

        public NotificationDAO(NotificationRepository repo, IMapper mapper)
        {
            _Repo = repo;
            _mapper = mapper;
        }
        public List<GetNotification> GetAll()
        {
            try
            {
                List<GetNotification> Notifications = this._mapper.Map<List<GetNotification>>(this._Repo.Get().ToList());
                return Notifications;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
