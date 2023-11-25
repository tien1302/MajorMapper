using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Payments
{
    public class CreatePayment
    {
        public int UserId { get; set; }

        public string OrderType { get; set; }

        public int RelatiedId { get; set; }

        public string OrderId { get; set; }

        public string TransactionId { get; set; }

        public int Amount { get; set; }

        public string Description { get; set; }
    }
}
