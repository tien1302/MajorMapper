using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Notifications;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
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

        public NotificationDAO(INotificationRepository repo, IMapper mapper)
        {
            _Repo = (NotificationRepository)repo;
            _mapper = mapper;
        }
        public List<GetNotification> GetAll()
        {
            try
            {
                List<GetNotification> notifications = this._mapper.Map<List<GetNotification>>(this._Repo.Get().ToList());
                return notifications;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetNotification Get(int key)
        {
            try
            {
                Notification notification = this._Repo.GetByID(key);
                if (notification == null)
                {
                    throw new Exception("Notification Id does not exist in the system.");
                }
                return this._mapper.Map<GetNotification>(notification);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Create(CreateNotification create)
        {
            try
            {
                Notification notification = new Notification()
                {
                    BookingId = create.BookingId,
                    NotificationContent = create.NotificationContent,
                    Title = create.Title,
                    Time = create.Time,
                    IsRead = create.IsRead,
                };
                this._Repo.Insert(notification);
                this._Repo.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(int key, UpdateNotification update)
        {
            try
            {
                Notification existedNotification = this._Repo.GetByID(key);
                if (existedNotification == null)
                {
                    throw new Exception("NotificationId does not exist in the system.");
                }

                existedNotification.BookingId = update.BookingId;
                existedNotification.NotificationContent = update.NotificationContent;
                existedNotification.Title = update.Title;
                existedNotification.Time = update.Time;
                existedNotification.IsRead = update.IsRead;
                this._Repo.Update(existedNotification);
                this._Repo.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int key)
        {
            try
            {
                Notification existedNotification = this._Repo.GetByID(key);
                if (existedNotification == null)
                {
                    throw new Exception("NotificationId does not exist in the system.");
                }
                this._Repo.Delete(key);
                this._Repo.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
