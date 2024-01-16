using BAL.Authentications;
using BAL.Services.Interfaces;
using BAL.DTOs.Payments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetPayment> listPayment = _paymentService.GetAll();
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
                GetPayment payment = _paymentService.Get(id);
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
        [HttpGet("GetByOrderId/{id}")]
        public IActionResult GetByOrderId(string id)
        {
            try
            {
                GetPayment payment = _paymentService.GetByOrderId(id);
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
                _paymentService.Create(createPayment);
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
                _paymentService.Delete(id);
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
                Tuple<List<int>, List<int>> money = _paymentService.Getmoney(year);
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
                Tuple<List<int>, List<int>> money = _paymentService.GetmoneybyId(id,year);
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
                var url = _paymentService.CreatePaymentUrl(model, HttpContext);

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
                var response = _paymentService.PaymentExecute(model);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
    }
}
