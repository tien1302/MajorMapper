using BAL.DAOs.Interfaces;
using BAL.DTOs.Slots;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        private ISlotDAO _slotDAO;

        public SlotController(ISlotDAO slotDAO)
        {
            _slotDAO = slotDAO;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetSlot> listSlot = _slotDAO.GetAll();
                return Ok(listSlot);
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
        public IActionResult Get(int id)
        {
            try
            {
                List<GetSlot> slot = _slotDAO.Get(id);
                return Ok(slot);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                GetSlot slot = _slotDAO.GetById(id);
                return Ok(slot);
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
        public IActionResult Post([FromBody] CreateSlot createSlot)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _slotDAO.Create(createSlot);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateSlot updateSlot)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _slotDAO.Update(id, updateSlot);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _slotDAO.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
