using BAL.Authentications;
using BAL.DAOs.Implementations;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Accounts;
using BAL.DTOs.Universities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IAccountDAO _DAO;

        public AccountController(IAccountDAO DAO)
        {
            _DAO = DAO;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetAccount> accounts = this._DAO.GetAll();
                return Ok(new
                {
                    Data = accounts
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
                GetAccount account = _DAO.Get(key);
                return Ok(new
                {
                    Data = account
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
        public IActionResult Post([FromBody] CreateAccount create)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _DAO.Create(create);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int key, [FromBody] UpdateAccount update)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _DAO.Update(key, update);
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
                _DAO.Delete(key);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
