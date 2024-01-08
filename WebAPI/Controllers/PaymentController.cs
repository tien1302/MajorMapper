using BAL.Authentications;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Payments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IPaymentDAO _paymentDAO;

        public PaymentController(IPaymentDAO paymentDAO)
        {
            _paymentDAO = paymentDAO;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetPayment> listPayment = _paymentDAO.GetAll();
                return Ok(listPayment);
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
                GetPayment payment = _paymentDAO.Get(id);
                return Ok(payment);
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
        public IActionResult Post([FromBody] CreatePayment createPayment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _paymentDAO.Create(createPayment);
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
                _paymentDAO.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Lấy số tiền cho admin
        [PermissionAuthorize("Admin")]
        [HttpGet("listMoney")]
        public IActionResult GetMoney(int year)
        {
            try
            {
                List<int> money = _paymentDAO.Getmoney(year);
                return Ok(money);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        //Lấy số tiền theo ConsultantId
        [PermissionAuthorize("Consultant")]
        [HttpGet("listMoneyById")]
        public IActionResult GetMoneyById(int id,int year)
        {
            try
            {
                Tuple<List<int>, List<int>> money = _paymentDAO.GetmoneybyId(id,year);
                return Ok(money);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        //Tạo url của Vnpay
        [HttpPost("CreatePaymentUrl")]
        public IActionResult CreatePaymentUrl([FromBody] CreatePayment model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var url = _paymentDAO.CreatePaymentUrl(model, HttpContext);

                return Ok(url);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Xử lý thông tin từ Vnpay
        [HttpPost("PaymentCallback")]
        public IActionResult PaymentCallback([FromBody] CreatePayment model)
        {
            try 
            {
                var response = _paymentDAO.PaymentExecute(model);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
    }
}
