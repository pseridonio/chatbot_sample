using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birra.API.Data.Entities
{
    public class Answer
    {
        public int AnswerID { get; set; }
        public string AnswerText { get; set; }

        public IList<RelationQuestionAnswer> RelationQuestionAnswers { get; set; }
    }
}
