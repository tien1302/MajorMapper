using BAL.Authentications;
using BAL.Services.Interfaces;
using BAL.DTOs.PersonalityTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalityTypeController : ControllerBase
    {
        private IPersonalityTypeService _personalityTypeService;

        public PersonalityTypeController(IPersonalityTypeService personalityTypeService)
        {
            _personalityTypeService = personalityTypeService;
        }

        [PermissionAuthorize("Admin", "Consultant")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetPersonalityType> listPersonalityType = _personalityTypeService.GetAll();
                return Ok(listPersonalityType);
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
        [HttpGet("GetAllByMethodId/{id}")]
        public IActionResult GetAllByMethodId(int methodId)
        {
            try
            {
                List<GetPersonalityType> listPersonalityType = _personalityTypeService.GetAllByMethodId(methodId);
                return Ok(listPersonalityType);
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
                GetPersonalityType personalityType = _personalityTypeService.Get(id);
                return Ok(personalityType);
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
        public IActionResult Post([FromBody] CreatePersonalityType createPersonalityType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _personalityTypeService.Create(createPersonalityType);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [PermissionAuthorize("Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdatePersonalityType updatePersonalityType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _personalityTypeService.Update(id, updatePersonalityType);
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
                _personalityTypeService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
