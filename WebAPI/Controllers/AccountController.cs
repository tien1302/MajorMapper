using BAL.Authentications;
using BAL.DAOs.Implementations;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Accounts;
using BAL.DTOs.Authentications;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IAccountDAO _DAO;
        private IOptions<JwtAuth> _jwtAuthOptions;

        public AccountController(IAccountDAO DAO, IOptions<JwtAuth> jwtAuthOptions)
        {
            _DAO = DAO;
            _jwtAuthOptions = jwtAuthOptions;
        }
        
        [HttpGet]
        //[PermissionAuthorize("Admin")]
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
        public IActionResult Get(int id)
        {
            try
            {
                GetAccount account = _DAO.Get(id);
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
        public IActionResult Put(int id, [FromBody] UpdateAccount update)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _DAO.Update(id, update);
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
                _DAO.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public IActionResult Post([FromBody] AuthenticationAccount authenAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                GetAccount getAccount = this._DAO.Login(authenAccount, this._jwtAuthOptions.Value);
                return Ok(new
                {
                    Data = getAccount
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
    }
}
