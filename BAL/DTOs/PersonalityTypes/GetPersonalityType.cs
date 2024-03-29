﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.PersonalityTypes
{
    public class GetPersonalityType
    {
        [Key]
        public int Id { get; set; }

        public int MethodId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string MethodName { get; set; }
        
    }
}
