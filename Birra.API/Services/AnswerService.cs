using Birra.API.Data;
using Birra.API.Data.Entities;
using Birra.API.DTOs.Answers;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birra.API.Services
{
    public interface IAnswerService
    {
        public Task<AnswerResponse> RegisterAnswer(string sessionID, string answer);
    }

    public class AnswerService: IAnswerService
    {
        private readonly BirraDataContext _context;

        public AnswerService(BirraDataContext dataContext)
        {
            this._context = dataContext;
        }

        public async Task<AnswerResponse> RegisterAnswer(string sessionID, string answerText)
        {
            AnswerResponse response = new AnswerResponse();

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Decisão de obter os objetos em vários métodos tomada devido ao banco SQLite.
                    // Utilizando bancos relacionais seria possível utilizar eager loading, até mesmo utilizando split de comandos SQL para carregamento das informações.
                    CurrentSession currentSession = GetCurrentSession(sessionID);

                    Question currentQuestion = this.GetCurrentQuestion(currentSession);

                    Answer answer = this.GetQuestionAnswer(answerText);

                    RelationQuestionAnswer questionAnswer = this.GetQuestionAnswer(currentQuestion, answer);

                    if (questionAnswer != null)
                    {
                        currentSession.CurrentQuestionID = questionAnswer.NextQuestionID ?? 0;

                        if (questionAnswer.MessageID.HasValue)
                        {
                            Message message = this.GetMessage(questionAnswer.MessageID.Value);
                            response.AnswerMessage = message.MessageText;
                        }
                    }

                    currentSession.LastInteraction = DateTime.Now;

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    //ToDo: Colocar log de erros
                    throw;
                }
            }

            return response;
        }

        private Message GetMessage(int messageID)
        {
            IQueryable<Message> messagesQuery = _context.Messages.AsQueryable();
            messagesQuery = messagesQuery.Where(message => message.MessageID == messageID);
            Message currentMessage = messagesQuery.FirstOrDefault();
            return currentMessage;
        }

        private Answer GetQuestionAnswer(string answerText)
        {
            IQueryable<Answer> answers = _context.Answers.AsQueryable();
            answers = answers.Where(answer => answer.AnswerText == answerText);
            Answer currentAnswer = answers.FirstOrDefault();
            return currentAnswer;
        }

        private RelationQuestionAnswer GetQuestionAnswer(Question currentQuestion, Answer answer)
        {
            IQueryable<RelationQuestionAnswer> relationQuestionAnswersQuery = _context.RelationQuestionAnswers.AsQueryable();
            relationQuestionAnswersQuery = relationQuestionAnswersQuery.Where(relation => relation.QuestionID == currentQuestion.QuestionID);
            relationQuestionAnswersQuery = relationQuestionAnswersQuery.Where(relation => relation.AnswerID == answer.AnswerID);
            RelationQuestionAnswer questionAnswer = relationQuestionAnswersQuery.FirstOrDefault();

            return questionAnswer;
        }

        private CurrentSession GetCurrentSession(string sessionID)
        {
            IQueryable<Data.Entities.CurrentSession> currentSessionQuery = _context.CurrentSessions.AsQueryable();
            currentSessionQuery = currentSessionQuery.Where(currentSession => currentSession.CurrentSessionID == sessionID);
            currentSessionQuery = currentSessionQuery.Where(currentSession => currentSession.LastInteraction > DateTime.Now.AddMinutes(-30));

            Data.Entities.CurrentSession currentSession = currentSessionQuery.FirstOrDefault();

            if (currentSession == null)
                throw new ArgumentException("Invalid session id", "sessionID");
            return currentSession;
        }

        private Question GetCurrentQuestion(CurrentSession currentSession)
        {
            Question question;
            int currentQuestionID = currentSession.CurrentQuestionID;

            IQueryable<Data.Entities.Question> questionQuery = _context.Questions.AsQueryable();
            questionQuery = questionQuery.Where(question => question.QuestionID == currentQuestionID);
            question = questionQuery.FirstOrDefault();
            return question;
        }
    }
}
