using BAL.DAOs.Implementations;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Majors;
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
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                GetMajor major = _majorDAO.Get(id);
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
        public IActionResult Put([FromRoute] int id, [FromBody] UpdateMajor updateMajor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _majorDAO.Update(id, updateMajor);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromForm] int id)
        {
            try
            {
                _majorDAO.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
