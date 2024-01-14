using BAL.Authentications;
using BAL.Services.Implementations;
using BAL.Services.Interfaces;
using BAL.DTOs.Accounts;
using BAL.DTOs.Authentications;
using BAL.DTOs.TestResults;
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
        public IAccountService _Service;
        private IOptions<JwtAuth> _jwtAuthOptions;

        public AccountController(IAccountService Service, IOptions<JwtAuth> jwtAuthOptions)
        {
            _Service = Service;
            _jwtAuthOptions = jwtAuthOptions;
        }

        [PermissionAuthorize("Admin", "Consultant")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetAccount> accounts = this._Service.GetAll();
                return Ok(accounts);
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
                GetAccount account = _Service.Get(id);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [PermissionAuthorize("Admin","Consultant", "Player")]
        [HttpGet("GetTestResult/{id}")]
        public IActionResult GetTestResult(int id)
        {
            try
            {
                List<GetTestResult> list = _Service.GetTestResultbyAccountId(id);
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
        [HttpPost]
        public IActionResult Post([FromBody] CreateAccount create)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _Service.Create(create);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [PermissionAuthorize("Consultant", "Player", "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateAccount update)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _Service.Update(id, update);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [PermissionAuthorize("Consultant", "Player")]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPassword reset)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _Service.ResetPassword(reset);
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
                _Service.Delete(id);
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
                GetAccount getAccount = this._Service.Login(authenAccount, this._jwtAuthOptions.Value);
                return Ok(getAccount);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    ex.Message
                );
            }
        }

        [HttpPost("LoginGoogle")]
        public IActionResult Google([FromBody] AuthenticationAccountGoogle authenAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                GetAccount getAccount = this._Service.LoginGoogle(authenAccount, this._jwtAuthOptions.Value);
                return Ok(getAccount);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    ex.Message
                );
            }
        }
    }
}
