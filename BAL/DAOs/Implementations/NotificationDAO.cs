using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Notifications;
using BAL.DTOs.Slots;
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
        private SlotRepository _slotRepository;
        private IMapper _mapper;

        public NotificationDAO(INotificationRepository repo, ISlotRepository slotRepository, IMapper mapper)
        {
            _Repo = (NotificationRepository)repo;
            _slotRepository = (SlotRepository)slotRepository;
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

        public List<GetNotification> GetAllByConsultantId(int key)
        {
            try
            {
                List<GetNotification> notifications = this._mapper.Map<List<GetNotification>>(this._Repo.Get(filter: n => n.Booking.Slot.ConsultantId == key, 
                    includeProperties: "Booking.Slot", orderBy: q => q.OrderDescending()).Take(15).ToList());
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
        public void Create(int? bookingId)
        {
            try
            {
                Slot slot = _slotRepository.Get(filter: s => s.Bookings.Any(b => b.Id == bookingId), includeProperties: "Bookings").FirstOrDefault();

                if(slot == null)
                {
                    throw new Exception("Slot does not exist");
                }

                Notification notification = new Notification()
                {
                    BookingId = bookingId,
                    NotificationContent = $"Bạn có một lịch tư vấn mới vào lúc{slot.StartDateTime.ToString("HH:mm")} ngày {slot.StartDateTime.ToString("MM/dd/yyyy")}",
                    Title = "Có lịch tư vấn mới",
                    Time = DateTime.Now,
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

        //Get notifications by consultantId
        public List<GetNotification> GetAllByConsultantId(string key)
        {
            try
            {
                List<GetNotification> notifications = this._mapper.Map<List<GetNotification>>(this._Repo.Get(filter: n => n.Booking.Slot.ConsultantId == int.Parse(key), includeProperties: "Booking.Slot").ToList());
                return notifications;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
