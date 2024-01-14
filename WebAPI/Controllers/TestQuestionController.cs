using BAL.Authentications;
using BAL.Services.Implementations;
using BAL.Services.Interfaces;
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
        private ITestQuestionService _testQuestionService;

        public TestQuestionController(ITestQuestionService testQuestionService)
        {
            _testQuestionService = testQuestionService;
        }

        [PermissionAuthorize("Player")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetTestQuestion> list = _testQuestionService.GetAll();
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
                GetTestQuestion testQuestion = _testQuestionService.Get(id);
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
                _testQuestionService.Create(create);
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
                _testQuestionService.Update(id, update);
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
                _testQuestionService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
