using BAL.DTOs.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IBookingService
    {
        public List<GetBooking> GetAll();
        public GetBooking Get(int key);
        public GetBooking Create(CreateBooking create);
        public void Update(int key, UpdateBooking update);
    }
}
