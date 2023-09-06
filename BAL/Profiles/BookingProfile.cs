using AutoMapper;
using BAL.DTOs.Accounts;
using BAL.DTOs.Bookings;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, GetBooking>().ReverseMap();
        }
    }
}
