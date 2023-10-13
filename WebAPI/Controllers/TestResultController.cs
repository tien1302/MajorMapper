using BAL.DAOs.Implementations;
using BAL.DAOs.Interfaces;
using BAL.DTOs.TestResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestResultController : ControllerBase
    {
        private ITestResultDAO _testResultDAO;

        public TestResultController(ITestResultDAO testResultDAO)
        {
            _testResultDAO = testResultDAO;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetTestResult> list = _testResultDAO.GetAll();
                return Ok(new
                {
                    Data = list
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
        public IActionResult Get(int id)
        {
            try
            {
                GetTestResult testResult = _testResultDAO.Get(id);
                return Ok(new
                {
                    Data = testResult
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
        public IActionResult Post([FromBody] CreateTestResult create)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _testResultDAO.Create(create);
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
                _testResultDAO.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
