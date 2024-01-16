using BAL.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Slots
{
    public class CreateSlot
    {
        public int ConsultantId { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public DateTime Date {  get; set; }
        public DateTime StartDateTime { get; set; }

        public bool AllDay { get; set; }

        public int Auto {  get; set; }
    }
}
