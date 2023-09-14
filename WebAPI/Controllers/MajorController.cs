using BAL.DAOs.Implementations;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Majors;
using BAL.DTOs.Universities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorController : ControllerBase
    {
        private IMajorDAO _majorDAO;

        public MajorController(IMajorDAO majorDAO)
        {
            _majorDAO = majorDAO;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetMajor> listMajor = _majorDAO.GetAll();
                return Ok(new
                {
                    Data = listMajor
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
                GetMajor major = _majorDAO.Get(key);
                return Ok(new
                {
                    Data = major
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
        public IActionResult Post([FromBody] CreateMajor createMajor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _majorDAO.Create(createMajor);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int key, [FromBody] UpdateMajor updateMajor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _majorDAO.Update(key, updateMajor);
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
                _majorDAO.Delete(key);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
