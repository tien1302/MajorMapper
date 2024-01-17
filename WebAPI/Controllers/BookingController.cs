using BAL.Authentications;
using BAL.Services.Interfaces;
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
        public IBookingService _Service;

        public BookingController(IBookingService Service)
        {
            _Service = Service;
        }
        [PermissionAuthorize("Player")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetBooking> bookings = this._Service.GetAll();
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

        [PermissionAuthorize("Player", "Consultant")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                GetBooking booking = _Service.Get(id);
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

        [PermissionAuthorize("Player", "Consultant")]
        [HttpPost]
        public IActionResult Post([FromBody] CreateBooking create)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                GetBooking booking = _Service.Create(create);
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
