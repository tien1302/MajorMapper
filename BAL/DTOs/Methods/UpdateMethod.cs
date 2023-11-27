using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Methods
{
    public class UpdateMethod
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime CreateDateTime { get; set; }
    }
}
