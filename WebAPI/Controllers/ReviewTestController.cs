using BAL.DAOs.Interfaces;
using BAL.DTOs.ReviewTests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewTestController : ControllerBase
    {
        private IReviewTestDAO _reviewTestDAO;

        public ReviewTestController(IReviewTestDAO reviewTestDAO)
        {
            _reviewTestDAO = reviewTestDAO;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetReviewTest> list = _reviewTestDAO.GetAll();
                return Ok(list);
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
                GetReviewTest reviewTest = _reviewTestDAO.Get(id);
                return Ok(reviewTest);
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
        public IActionResult Post([FromBody] CreateReviewTest create)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _reviewTestDAO.Create(create);
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
                _reviewTestDAO.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
