using AutoMapper;
using BAL.Services.Interfaces;
using BAL.DTOs.Payments;
using BAL.DTOs.Slots;
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

namespace BAL.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private PaymentRepository _paymentRepository;
        private TestRepository _testRepository;
        private BookingRepository _bookingRepository;
        private SlotRepository _slotRepository;
        private INotificationService _notificationService;
        private IMapper _mapper;
        private IConfiguration _configuration;

        public PaymentService(IPaymentRepository paymentRepository, ITestRepository testRepository, IBookingRepository bookingRepository, ISlotRepository slotRepository, INotificationService notificationService, IMapper mapper, IConfiguration configuration)
        {
            _paymentRepository = (PaymentRepository)paymentRepository;
            _testRepository = (TestRepository)testRepository;
            _bookingRepository = (BookingRepository)bookingRepository;
            _slotRepository = (SlotRepository)slotRepository;
            _notificationService = notificationService;
            _mapper = mapper;
            _configuration = configuration;
        }

        public void Create(CreatePayment createPayment)
        {
            try
            {
                Payment payment = new Payment()
                {
                    PlayerId = createPayment.PlayerId,
                    BookingId = createPayment.BookingId,
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

        //Tạo url của Vnpay
        public string CreatePaymentUrl(CreatePayment create, HttpContext context)
        {
            if (create.TestId > 0)
            {
                create.Amount = 50000;
                create.Description = "Thanh toán test's results";
            }
            else if (create.BookingId > 0)
            {
                create.Amount = 100000;
                create.Description = "Thanh toán booking";
            }

            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();
            var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];

            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", (create.Amount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);

            if (create.TestId > 0)
            {
                pay.AddRequestData("vnp_OrderType", "Test");
            }
            else if (create.BookingId > 0)
            {
                pay.AddRequestData("vnp_OrderType", "Booking");
            }

            pay.AddRequestData("vnp_OrderInfo", create.Description);
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return paymentUrl;
        }

        //Xử lý thông tin từ Vnpay
        public CreatePayment PaymentExecute(CreatePayment model)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(model, _configuration["Vnpay:HashSecret"]);
            if (response != null)
            {
                Create(response);
                if (response.TestId > 0)
                {
                    Test test = _testRepository.GetByID(response.TestId);
                    if(test != null)
                    {
                        test.StatusPayment = true;
                        _testRepository.Update(test);
                        _testRepository.Commit();
                    }
                }
                else if(response.BookingId > 0)
                {
                    Booking booking = _bookingRepository.GetByID(response.BookingId);
                    if (booking != null)
                    {
                        booking.Status = "Finish";
                        _bookingRepository.Update(booking);
                        _bookingRepository.Commit();
                    }
                    Slot slot = _slotRepository.GetByID(booking.SlotId);
                    if (slot != null)
                    {
                        slot.Status = "Booked";
                        _slotRepository.Update(slot);
                        _slotRepository.Commit();
                    }
                    _notificationService.Create(response.BookingId);
                }
            }
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

        //Lấy số tiền theo ConsultantId
        public Tuple<List<int>, List<int>> GetmoneybyId(int id,int year)
        {
            try
            {
                List<int> listmoney = new List<int>();
                List<int> listcount = new List<int>();
                int money = 0;
                int count = 0;
                List<GetPayment> listPayment = _mapper.Map<List<GetPayment>>(_paymentRepository.Get(filter: p => p.CreateDateTime.Year == year && p.Booking.Slot.ConsultantId == id && p.BookingId > 0, includeProperties: "Booking.Slot").ToList());
                for (int i = 1; i <= 12; i++)
                {
                    List<GetPayment> list = listPayment.Where(list => list.CreateDateTime.Month == i).ToList();
                    foreach (var item in list)
                    {
                        money += (item.Amount/10)*9;
                        count++;
                    }
                    
                    listmoney.Add(money);
                    listcount.Add(count);
                    money = 0;
                    count = 0;
                }
                var tuple = new Tuple<List<int>, List<int>>(listmoney, listcount);
                return tuple;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Lấy số tiền cho admin
        public Tuple<List<int>, List<int>> Getmoney(int year)
        {
            try
            {
                List<int> listmoney = new List<int>();
                List<int> listtest = new List<int>();
                int money =0;
                int test = 0;
                List<GetPayment> listBooking = _mapper.Map<List<GetPayment>>(_paymentRepository.Get(filter: p => p.CreateDateTime.Year == year && p.BookingId > 0).ToList());
                List<GetPayment> listTest = _mapper.Map<List<GetPayment>>(_paymentRepository.Get(filter: p => p.CreateDateTime.Year == year && p.TestId > 0).ToList());
                for (int i = 1; i <= 12; i++)
                {
                    List<GetPayment> listBookingByMonth = listBooking.Where(list => list.CreateDateTime.Month == i).ToList();
                    List<GetPayment> listTestByMonth = listTest.Where(list => list.CreateDateTime.Month == i).ToList();
                    foreach (var item in listBookingByMonth)
                    {
                        money += item.Amount / 10;
                    }
                    foreach (var item in listTestByMonth)
                    {
                        test += item.Amount;
                    }
                    listmoney.Add(money);
                    listtest.Add(test);
                    money = 0;
                    test = 0;
                }
                var tuple = new Tuple<List<int>, List<int>>(listmoney, listtest);
                return tuple;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
