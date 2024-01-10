using BAL.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Accounts
{
    public class CreateAccount
    {
        [MaxLength(100, ErrorMessage = "Tên không vượt quá 100 ký tự.")]
        [Required(ErrorMessage = "Không được để trống")]
        [MinLength(5, ErrorMessage = "Tên không ít hơn 5 ký tự.")]
        [NameValidation(ErrorMessage = "Chữ cái đầu tiên của mỗi từ phải viết hoa.")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Không được để trống")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Không được để trống")]
        [Password("Password")]
        public string ConfirmPassword { get; set; } = null!;
        [Required(ErrorMessage = "Không được để trống")]
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [ComparationImportDateValidation(ErrorMessage = "Ngày sinh không vượt quá hiện tại.")]
        public DateTime? DoB { get; set; }

        public int RoleId { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [MaxLength(200, ErrorMessage = "Địa chỉ không vượt quá 100 ký tự.")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [PhoneAtribute(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? Phone { get; set; }
    }
}
