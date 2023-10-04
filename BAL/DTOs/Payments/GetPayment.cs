using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Payments
{
    public class GetPayment
    {
        [Key]
        public int Id { get; set; }

        public string OrderType { get; set; }

        public int OrderId { get; set; }

        public int TransactionId { get; set; }

        public int Amount { get; set; }

        public string Description { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
