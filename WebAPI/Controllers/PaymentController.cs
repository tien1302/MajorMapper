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

        //public IActionResult CreatePaymentUrl(CreatePayment model)
        //{
        //    var url = _paymentDAO.CreatePaymentUrl(model, HttpContext);

        //    return Redirect(url);
        //}

        //public IActionResult PaymentCallback()
        //{
        //    var response = _paymentDAO.PaymentExecute(Request.Query);

        //    return Json(response);
        //}

    }
}
