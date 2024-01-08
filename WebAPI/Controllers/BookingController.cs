using BAL.Authentications;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Bookings;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        public IBookingDAO _DAO;

        public BookingController(IBookingDAO DAO)
        {
            _DAO = DAO;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetBooking> bookings = this._DAO.GetAll();
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [PermissionAuthorize("Player")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                GetBooking booking = _DAO.Get(id);
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [PermissionAuthorize("Player")]
        [HttpPost]
        public IActionResult Post([FromBody] CreateBooking create)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                GetBooking booking = _DAO.Create(create);
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
