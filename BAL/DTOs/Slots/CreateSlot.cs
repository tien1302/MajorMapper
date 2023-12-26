using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Slots
{
    public class CreateSlot
    {
        public int ConsultantId { get; set; }

        public DateTime Date {  get; set; } 

        public DateTime StartDateTime { get; set; }
        //public string Recurring { get; set; }
        public bool AllDay { get; set; }
        public int Auto {  get; set; }
    }
}
