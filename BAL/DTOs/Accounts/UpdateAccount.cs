using BAL.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Accounts
{
    public class UpdateAccount
    {
        [MaxLength(100, ErrorMessage = "Tên không vượt quá 100 ký tự.")]
        [NameValidation(ErrorMessage = "Chữ cái đầu tiên của mỗi từ phải viết hoa.")]
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Gender { get; set; }
        [ComparationImportDateValidation(ErrorMessage = "Ngày sinh không vượt quá hiện tại.")]
        public DateTime? DoB { get; set; }
        [MaxLength(200, ErrorMessage = "Địa chỉ không vượt quá 100 ký tự.")]
        public string? Address { get; set; }
        [PhoneAtribute(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? Phone { get; set; }

        public bool Status { get; set; } 
    }
}
