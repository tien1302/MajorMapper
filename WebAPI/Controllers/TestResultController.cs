using BAL.Authentications;
using BAL.Services.Interfaces;
using BAL.DTOs.TestResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestResultController : ControllerBase
    {
        private ITestResultService _testResultService;

        public TestResultController(ITestResultService testResultService)
        {
            _testResultService = testResultService;
        }

        [PermissionAuthorize("Consultant", "Player")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetTestResult> list = _testResultService.GetAll();
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

        [PermissionAuthorize("Consultant", "Player")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                GetTestResult testResult = _testResultService.Get(id);
                return Ok(testResult);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        //Lấy test result theo methodName và testId
        [PermissionAuthorize("Player")]
        [HttpGet("GetByMethodAndTest")]
        public IActionResult GetByMethodAndTest(string methodName, int testId)
        {
            try
            {
                GetTestResult testResult = _testResultService.GetByMethodAndTest(methodName, testId);
                return Ok(testResult);
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
        public IActionResult Post([FromBody] CreateTestResult create)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _testResultService.Create(create);
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
                _testResultService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
