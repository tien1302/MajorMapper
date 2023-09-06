using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Bookings;
using DAL.Repositories.Implementations;
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

        public BookingDAO(BookingRepository repo, IMapper mapper)
        {
            _Repo = repo;
            _mapper = mapper;
        }
        public List<GetBooking> GetAll()
        {
            try
            {
                List<GetBooking> Bookings = this._mapper.Map<List<GetBooking>>(this._Repo.Get().ToList());
                return Bookings;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

