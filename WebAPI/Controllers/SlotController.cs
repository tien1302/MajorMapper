using BAL.Authentications;
using BAL.Services.Interfaces;
using BAL.DTOs.Slots;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        private ISlotService _slotService;

        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetSlot> listSlot = _slotService.GetAll();
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
                List<GetSlot> slot = _slotService.CheckStatus();
                slot = _slotService.Get(id);
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

        [PermissionAuthorize("Player", "Consultant")]
        // Hàm lấy list slot trống theo consultantId cho mobile
        [HttpGet("GetSlotActive")]
        public IActionResult GetSlotActive()
        {
            try
            {
                _slotService.CheckStatus();
                List<GetSlot> slot = _slotService.GetAllSlotActive();
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
                GetSlot slot = _slotService.GetBySlotId(id);
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
                _slotService.Create(createSlot);
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
                _slotService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _slotService.Update(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
