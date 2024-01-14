using BAL.Authentications;
using BAL.Services.Interfaces;
using BAL.DTOs.Majors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorController : ControllerBase
    {
        private IMajorService _majorService;

        public MajorController(IMajorService majorService)
        {
            _majorService = majorService;
        }

        [PermissionAuthorize("Admin", "Consultant")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetMajor> listMajor = _majorService.GetAll();
                return Ok(listMajor);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [PermissionAuthorize("Admin", "Consultant")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                GetMajor major = _majorService.Get(id);
                return Ok(major);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [PermissionAuthorize("Admin")]
        [HttpPost]
        public IActionResult Post([FromBody] CreateMajor createMajor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _majorService.Create(createMajor);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [PermissionAuthorize("Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateMajor updateMajor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _majorService.Update(id, updateMajor);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [PermissionAuthorize("Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _majorService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
