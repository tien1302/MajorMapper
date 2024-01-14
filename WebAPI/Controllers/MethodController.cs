using BAL.Authentications;
using BAL.Services.Interfaces;
using BAL.DTOs.Methods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MethodController : ControllerBase
    {
        private IMethodService _methodService;

        public MethodController(IMethodService methodService)
        {
            _methodService = methodService;
        }

        [PermissionAuthorize("Admin", "Consultant")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetMethod> list = _methodService.GetAll();
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

        [PermissionAuthorize("Admin", "Consultant")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                GetMethod method = _methodService.Get(id);
                return Ok(method);
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
        [HttpPost]
        public IActionResult Post([FromBody] CreateMethod create)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _methodService.Create(create);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [PermissionAuthorize("Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateMethod update)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _methodService.Update(id, update);
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
                _methodService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
