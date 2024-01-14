using AutoMapper;
using BAL.Services.Interfaces;
using BAL.DTOs.Bookings;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private BookingRepository _Repo;
        private IMapper _mapper;

        public BookingService(IBookingRepository repo, IMapper mapper)
        {
            _Repo = (BookingRepository)repo;
            _mapper = mapper;
        }

        public List<GetBooking> GetAll()
        {
            try
            {
                List<GetBooking> bookings = this._mapper.Map<List<GetBooking>>(this._Repo.Get().ToList());
                return bookings;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetBooking Get(int key)
        {
            try
            {
                GetBooking booking = this._mapper.Map<GetBooking>(this._Repo.Get(filter: x=>x.SlotId == key).FirstOrDefault());
                if (booking == null)
                {
                    throw new Exception("Booking does not exist in the system.");
                }
                return booking;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetBooking Create(CreateBooking create)
        {
            try
            {
                Booking booking = new Booking()
                {
                    PlayerId = create.PlayerId,
                    SlotId = create.SlotId,
                    Status = "Progressing",
                    CreateDateTime = DateTime.Now
                };

                this._Repo.Insert(booking);
                this._Repo.Commit();
                return this._mapper.Map<GetBooking>(booking);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int key, UpdateBooking update)
        {
            try
            {
                Booking existedBooking = this._Repo.GetByID(key);
                if (existedBooking == null)
                {
                    throw new Exception("BookingId does not exist in the system.");
                }

                existedBooking.PlayerId = update.PlayerId;
                existedBooking.SlotId = update.SlotId;
                existedBooking.Status = update.Status;
                this._Repo.Update(existedBooking);
                this._Repo.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

