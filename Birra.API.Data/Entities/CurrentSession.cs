using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birra.API.Data.Entities
{
    public class CurrentSession
    {
        public string CurrentSessionID { get; set; }
        public DateTime LastInteraction { get; set; }
        public int CurrentQuestionID { get; set; }
    }
}
