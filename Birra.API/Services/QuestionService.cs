using Birra.API.Data;
using Birra.API.Data.Entities;
using Birra.API.DTOs.Initialization;
using Birra.API.DTOs.Qestions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birra.API.Services
{
    public interface IQuestionService
    {
        Task<InitializationResponse> InitializeQuestions();
        
        Task<Data.Entities.Question> GetQuestion(string sessionID);
    }
    public class QuestionService : IQuestionService
    {
        private readonly BirraDataContext _context;
        private readonly SystemParametersService _systemParametersService;

        public QuestionService(BirraDataContext dataContext, SystemParametersService systemParametersService)
        {
            this._context = dataContext;
            this._systemParametersService = systemParametersService;
        }
        
        public async Task<Data.Entities.Question> GetQuestion(string sessionID)
        {
            Data.Entities.Question question = null;

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    CurrentSession currentSession = GetCurrentSession(sessionID);
                    
                    question = GetCurrentQuestion(currentSession);
                    
                    currentSession.LastInteraction = DateTime.Now;
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // ToDo: incluir log de erros
                    throw;
                }
            }

            return question;
        }

        public async Task<InitializationResponse> InitializeQuestions()
        {
            InitializationResponse response = new InitializationResponse();
            
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    IQueryable<Data.Entities.Message> messagesQuery = _context.Messages.AsQueryable();
                    messagesQuery = messagesQuery.Where(message => message.MessageID == _systemParametersService.WelcomeMessage);
                    Data.Entities.Message welcomeMessage = messagesQuery.FirstOrDefault();

                    Data.Entities.CurrentSession currentSession = new Data.Entities.CurrentSession()
                    {
                        CurrentSessionID = Guid.NewGuid().ToString(),
                        LastInteraction = DateTime.Now,
                        CurrentQuestionID = _systemParametersService.FirstQuestionID
                    };

                    _context.CurrentSessions.Add(currentSession);
                    await _context.SaveChangesAsync();
                    transaction.Commit();

                    response.SessionID = currentSession.CurrentSessionID;
                    response.WelcomeMessage = welcomeMessage.MessageText;
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    //ToDo: Incluir log de erros
                    throw;
                }
            }

            return response;
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
    }
}
