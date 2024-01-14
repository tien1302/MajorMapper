using BAL.Authentications;
using BAL.Services.Interfaces;
using BAL.DTOs.Scores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private IScoreService _scoreService;

        public ScoreController(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetScore> list = _scoreService.GetAll();
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
                GetScore score = _scoreService.Get(id);
                return Ok(score);
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
        public IActionResult Post([FromBody] CreateScore create)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _scoreService.Create(create);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [PermissionAuthorize("Player")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateScore update)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _scoreService.Update(id, update);
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
                _scoreService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
