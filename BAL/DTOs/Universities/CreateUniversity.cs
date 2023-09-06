using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Universities
{
    public class CreateUniversity
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public int Phone { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public string Icon { get; set; }
    }
}
