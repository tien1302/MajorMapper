using BAL.Authentications;
using BAL.DAOs.Implementations;
using BAL.DAOs.Interfaces;
using BAL.DTOs.TestQuestions;
using BAL.DTOs.Tests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestQuestionController : ControllerBase
    {
        private ITestQuestionDAO _testQuestionDAO;

        public TestQuestionController(ITestQuestionDAO testQuestionDAO)
        {
            _testQuestionDAO = testQuestionDAO;
        }

        [PermissionAuthorize("Player")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetTestQuestion> list = _testQuestionDAO.GetAll();
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

        [PermissionAuthorize("Player")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                GetTestQuestion testQuestion = _testQuestionDAO.Get(id);
                return Ok(testQuestion);
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
        [HttpPost]
        public IActionResult Post([FromBody] CreateTestQuestion create)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _testQuestionDAO.Create(create);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [PermissionAuthorize("Player")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateTestQuestion update)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _testQuestionDAO.Update(id, update);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [PermissionAuthorize("Player")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _testQuestionDAO.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
