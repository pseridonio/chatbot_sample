using Birra.API.DTOs.Answers;
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
    public class AnswersController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswersController(IAnswerService answerService)
        {
            this._answerService = answerService;
        }

        [HttpPost]
        public async Task<ActionResult<AnswerResponse>> RegisterResponse(AnswerRequest request)
        {
            AnswerResponse response = null;

            try
            {
                response = await _answerService.RegisterAnswer(request.SessionID, request.AnswerText);
            }
            catch (Exception ex)
            {
                //ToDo: Incluir log de erros

                response = new AnswerResponse()
                {
                    Error = new DTOs.ErrorDTO()
                    {
                        ErrorCode = 3,
                        ErrorMessage = ex.Message
                    }
                };

                return BadRequest(response);
            }

            return response;
        }
    }
}
