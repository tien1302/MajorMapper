using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Bookings;
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
    public class BookingDAO : IBookingDAO
    {
        private BookingRepository _Repo;
        private IMapper _mapper;

        public BookingDAO(IBookingRepository repo, IMapper mapper)
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
                Booking booking = this._Repo.GetByID(key);
                if (booking == null)
                {
                    throw new Exception("Booking Id does not exist in the system.");
                }
                return this._mapper.Map<GetBooking>(booking);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Create(CreateBooking create)
        {
            try
            {
                Booking booking = new Booking()
                {
                    UserId = create.UserId,
                    SlotId = create.SlotId,
                    Status = "Đã đặt",
                    CreateDateTime = DateTime.Now
                };
                this._Repo.Insert(booking);
                this._Repo.Commit();
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

                existedBooking.UserId = update.UserId;
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

        public void Delete(int key)
        {
            try
            {
                Booking existedBooking = this._Repo.GetByID(key);
                if (existedBooking == null)
                {
                    throw new Exception("BookingId does not exist in the system.");
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

