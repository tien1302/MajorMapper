using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ValidationAttributes
{
    public class valiSlot : ValidationAttribute
    {
        public valiSlot()
        {
        }
        public override bool IsValid(object? value)
        {
            DateTime dateTime;
            if (DateTime.TryParse(value.ToString(), out dateTime) && dateTime.CompareTo(DateTime.Today)>= 0)
            {
                return true;
            }
            return false;
        }
    }
}
