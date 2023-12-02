using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Payments;
using BAL.VnPay;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Implementations
{
    public class PaymentDAO : IPaymentDAO
    {
        private PaymentRepository _paymentRepository;
        private IMapper _mapper;
        private readonly IConfiguration _configuration;

        public PaymentDAO(IPaymentRepository paymentRepository, IMapper mapper, IConfiguration configuration)
        {
            _paymentRepository = (PaymentRepository)paymentRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public void Create(CreatePayment createPayment)
        {
            try
            {
                Payment payment = new Payment()
                {
                    UserId = createPayment.UserId,
                    OrderType = createPayment.OrderType,
                    OrderId = createPayment.OrderId,
                    TransactionId = createPayment.TransactionId,
                    Amount = createPayment.Amount,
                    Description = createPayment.Description,
                    CreateDateTime = DateTime.Now,
                };
                _paymentRepository.Insert(payment);
                _paymentRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string CreatePaymentUrl(CreatePayment create, HttpContext context)
        {
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();
            var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];

            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("userId", create.UserId.ToString());
            pay.AddRequestData("relatiedId", create.RelatiedId.ToString());
            pay.AddRequestData("vnp_Amount", (create.Amount * 100).ToString());
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", $"{create.Description}");
            pay.AddRequestData("orderType", create.OrderType);
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return paymentUrl;
        }

        public CreatePayment PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);
            if(response != null)
                Create(response);
            return response;
        }

        public void Delete(int key)
        {
            try
            {
                Payment existedPayment = _paymentRepository.GetByID(key);
                if (existedPayment == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }
                _paymentRepository.Delete(key);
                _paymentRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetPayment Get(int key)
        {
            try
            {
                List<GetPayment> listPayment = _mapper.Map<List<GetPayment>>(_paymentRepository.Get().ToList());
                Payment payment = _paymentRepository.GetByID(key);

                if (payment == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                GetPayment result = listPayment.FirstOrDefault(p => p.Id == payment.Id);
                return _mapper.Map<GetPayment>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetPayment> GetAll()
        {
            try
            {
                List<GetPayment> listPayment = _mapper.Map<List<GetPayment>>(_paymentRepository.Get().ToList());
                return listPayment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
