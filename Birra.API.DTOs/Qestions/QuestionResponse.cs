﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birra.API.DTOs.Qestions
{
    public class QuestionResponse : BaseResponseDTO
    {
        public string Question { get; set; }
        public string Hint { get; set; }
    }
}