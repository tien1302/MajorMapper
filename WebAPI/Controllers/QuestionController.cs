using BAL.Authentications;
using BAL.Services.Interfaces;
using BAL.DTOs.Accounts;
using BAL.DTOs.Questions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [PermissionAuthorize("Admin", "Consultant")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetQuestion> list = _questionService.GetAll();
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

        [PermissionAuthorize("Admin")]
        [HttpGet("GetProcessing")]
        public IActionResult GetProcessing()
        {
            try
            {
                List<GetQuestion> question = _questionService.GetProcessing();
                return Ok(question);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [PermissionAuthorize("Admin", "Consultant", "Player")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                GetQuestion question = _questionService.Get(id);
                return Ok(question);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [PermissionAuthorize("Consultant")]
        [HttpPost]
        public IActionResult Post([FromBody] CreateQuestion create)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _questionService.Create(create);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [PermissionAuthorize("Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateQuestion update)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _questionService.Update(id, update);
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
                _questionService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
