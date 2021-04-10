using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birra.API.Data.Entities
{
    public class Message
    {
        public int MessageID { get; set; }
        public string MessageText { get; set; }

        public IList<RelationQuestionAnswer> RelationQuestionAnswers { get; set; }
    }
}
