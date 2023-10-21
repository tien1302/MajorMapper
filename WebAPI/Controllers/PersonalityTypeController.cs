using BAL.DAOs.Interfaces;
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
        private IPersonalityTypeDAO _personalityTypeDAO;

        public PersonalityTypeController(IPersonalityTypeDAO personalityTypeDAO)
        {
            _personalityTypeDAO = personalityTypeDAO;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetPersonalityType> listPersonalityType = _personalityTypeDAO.GetAll();
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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                GetPersonalityType personalityType = _personalityTypeDAO.Get(id);
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

        [HttpPost]
        public IActionResult Post([FromBody] CreatePersonalityType createPersonalityType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _personalityTypeDAO.Create(createPersonalityType);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdatePersonalityType updatePersonalityType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _personalityTypeDAO.Update(id, updatePersonalityType);
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
                _personalityTypeDAO.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
