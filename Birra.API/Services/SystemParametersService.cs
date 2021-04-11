using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birra.API.Services
{
    public class SystemParametersService//: ISystemParametersService
    {
        public int FirstQuestionID { get; set; }
        public int WelcomeMessage { get; set; }
        public int WrongAnswerMessageID { get; set; }

        public SystemParametersService(IConfiguration configuration)
        {
            this.FirstQuestionID = ((int?)configuration.GetValue(typeof(int?), "SystemParameters:FirstQuestion") ?? 0);
            this.WelcomeMessage = ((int?)configuration.GetValue(typeof(int?), "SystemParameters:WelcomeMessage") ?? 0);
            this.WrongAnswerMessageID = ((int?)configuration.GetValue(typeof(int?), "SystemParameters:WrongAnswerMessage") ?? 0);
        }
    }
}
