using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BAL.ValidationAttributes
{
    public class PasswordAttribute : ValidationAttribute
    {
        private readonly string _passwordPropertyName;

        public PasswordAttribute(string passwordPropertyName)
        {
            _passwordPropertyName = passwordPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = (string)validationContext.ObjectType.GetProperty(_passwordPropertyName).GetValue(validationContext.ObjectInstance, null);
            var confirmPassword = (string)value;

            if (password != confirmPassword)
            {
                return new ValidationResult("Xác nhận mật khẩu không đúng.");
            }

            return ValidationResult.Success;
        }
    }
}
