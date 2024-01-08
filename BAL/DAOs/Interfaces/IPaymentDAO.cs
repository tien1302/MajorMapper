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
        //Tạo url của Vnpay
        string CreatePaymentUrl(CreatePayment create, HttpContext context);
        //Xử lý thông tin từ Vnpay
        CreatePayment PaymentExecute(CreatePayment create);
        //Lấy số tiền theo ConsultantId
        public List<int> Getmoney(int year);
        //Lấy số tiền cho admin
        public Tuple<List<int>, List<int>> GetmoneybyId(int id, int year);
    }
}
