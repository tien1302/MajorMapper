using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Payments;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
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

        public PaymentDAO(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = (PaymentRepository)paymentRepository;
            _mapper = mapper;
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
