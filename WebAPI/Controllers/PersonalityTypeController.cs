using BAL.DAOs.Implementations;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Majors;
using BAL.DTOs.PersonalityTypes;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

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
                return Ok(new
                {
                    Data = listPersonalityType
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
                GetPersonalityType personalityType = _personalityTypeDAO.Get(key);
                return Ok(new
                {
                    Data = personalityType
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
        public IActionResult Put([FromRoute] int key, [FromBody] UpdatePersonalityType updatePersonalityType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _personalityTypeDAO.Update(key, updatePersonalityType);
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
                _personalityTypeDAO.Delete(key);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
