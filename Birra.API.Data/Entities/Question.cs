using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birra.API.Data.Entities
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string QuestionHint { get; set; }

        public IList<RelationQuestionAnswer> RelationQuestionAnswers { get; set; }

        public IList<RelationQuestionAnswer> PreviewsRelationQuestionAnswers { get; set; }
    }
}
