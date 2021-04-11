using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birra.API.DTOs.Initialization
{
    public class InitializationResponse: BaseResponseDTO
    {
        public string SessionID { get; set; }
        public string WelcomeMessage { get; set; }
    }
}
