using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Tests
{
    public class GetTest
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public bool Status { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
