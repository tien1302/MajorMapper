using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Questions
{
    public class CreateQuestion
    {
        public string AssetsName { get; set; }

        public int Type { get; set; }

        public string Description { get; set; }
    }
}
