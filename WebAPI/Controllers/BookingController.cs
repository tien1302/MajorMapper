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
                return Ok(new
                {
                    Data = bookings
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int key)
        {
            try
            {
                GetBooking booking = _DAO.Get(key);
                return Ok(new
                {
                    Data = booking
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateBooking create)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _DAO.Create(create);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int key, [FromBody] UpdateBooking update)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _DAO.Update(key, update);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromForm] int key)
        {
            try
            {
                _DAO.Delete(key);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
