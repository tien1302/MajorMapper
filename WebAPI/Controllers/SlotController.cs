using BAL.Authentications;
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

        [PermissionAuthorize("Consultant")]
        // Hàm lấy list slot theo consultantId
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                List<GetSlot> slot = _slotDAO.CheckStatus();
                slot = _slotDAO.Get(id);
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

        [PermissionAuthorize("Player")]
        // Hàm lấy list slot trống theo consultantId cho mobile
        [HttpGet("GetSlotActive")]
        public IActionResult GetSlotActive()
        {
            try
            {
                _slotDAO.CheckStatus();
                List<GetSlot> slot = _slotDAO.GetAllSlotActive();
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

        [PermissionAuthorize("Consultant")]
        // Hàm lấy slot theo SlotId
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                GetSlot slot = _slotDAO.GetBySlotId(id);
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

        [PermissionAuthorize("Consultant")]
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

        [PermissionAuthorize("Consultant")]
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
