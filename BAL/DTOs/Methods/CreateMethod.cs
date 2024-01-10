using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Methods
{
    public class CreateMethod
    {
        [Required(ErrorMessage = "Không được để trống")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Không được để trống")]
        public string Description { get; set; } = null!;

    }
}
