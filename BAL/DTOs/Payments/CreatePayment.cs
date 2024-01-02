using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Payments
{
    public class CreatePayment
    {
        public int PlayerId { get; set; }

        public int? BookingId { get; set; }

        public int? TestId { get; set; }

        public string? OrderId { get; set; }

        public string? TransactionId { get; set; }

        public int Amount { get; set; }

        public string Description { get; set; }

        public string? SecureHash { get; set; }
    }
}
