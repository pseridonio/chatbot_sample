using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birra.API.DTOs.Answers
{
    public class AnswerRequest : BaseRequestDTO
    {
        public string AnswerText { get; set; }
    }
}
