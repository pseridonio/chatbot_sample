using Birra.API.DTOs.Qestions;
using Birra.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            this._questionService = questionService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// 200 - There is a next question to the session
        /// 204 - There isn't a next question to the session
        /// 400 - An invalid session ID was informed
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<QuestionResponse>> GetNextQuestion(QuestionRequest request)
        {
            QuestionResponse questionResponse = new QuestionResponse();

            try
            {
                Data.Entities.Question question = await _questionService.GetQuestion(request.SessionID);

                // If there is no next question, no content will be returned
                if (question == null)
                    return NoContent();

                questionResponse.Question = question.QuestionText;
                questionResponse.Hint = question.QuestionHint;
            }
            catch (Exception ex)
            {
                questionResponse.Error = new DTOs.ErrorDTO()
                {
                    ErrorCode = 2,
                    ErrorMessage = ex.Message
                };

                return BadRequest(questionResponse);
            }

            return Ok(questionResponse);
        }
    }
}
