using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birra.API.Data.Entities
{
    public class RelationQuestionAnswer
    {
        public int QuestionID { get; set; }
        public int AnswerID { get; set; }
        public int? NextQuestionID { get; set; }
        public int? MessageID { get; set; }

        public Question Question { get; set; }
        public Answer Answer { get; set; }
        public Message Message { get; set; }
        public Question NextQuestion { get; set; }
    }
}
