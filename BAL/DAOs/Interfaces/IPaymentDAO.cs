using BAL.DTOs.Payments;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface IPaymentDAO
    {
        public List<GetPayment> GetAll();
        public GetPayment Get(int key);
        public void Create(CreatePayment createPayment);
        public void Delete(int key);
        string CreatePaymentUrl(CreatePayment create, HttpContext context);
        CreatePayment PaymentExecute(IQueryCollection collections);
    }
}
