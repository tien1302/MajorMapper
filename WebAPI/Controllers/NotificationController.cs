using BAL.DAOs.Interfaces;
using BAL.DTOs.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public INotificationDAO _DAO;

        public NotificationController(INotificationDAO DAO)
        {
            _DAO = DAO;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetNotification> notifications = this._DAO.GetAll();
                return Ok(new
                {
                    Data = notifications
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
                GetNotification notification = _DAO.Get(key);
                return Ok(new
                {
                    Data = notification
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
        public IActionResult Post([FromBody] CreateNotification create)
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
        public IActionResult Put([FromRoute] int key, [FromBody] UpdateNotification update)
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
