using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BAL.ValidationAttributes
{
    public class PhoneAtribute : ValidationAttribute
    {
        public PhoneAtribute()
        {
        }
        public override bool IsValid(object? value)
        {
            string regex = "^0[0-9]{9}$";
            Match match = Regex.Match(value.ToString(), regex);
            if (match.Success)
            {
                return true;
            }
            return false;

  
        }
    }
}
