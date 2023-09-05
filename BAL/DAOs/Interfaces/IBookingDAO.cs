using BAL.DTOs.Accounts;
using BAL.DTOs.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface IBookingDAO
    {
        public List<GetBooking> GetAll();
        public GetBooking Get(int key);
        public void Create(GetBooking create);
        public void Update(int key, GetBooking update);
        public void Delete(int key);
    }
}
