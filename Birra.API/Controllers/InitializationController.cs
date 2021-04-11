using Birra.API.Data;
using Birra.API.DTOs.Initialization;
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
    public class InitializationController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public InitializationController(IQuestionService questionService)
        {
            this._questionService = questionService;
        }

        public async Task<ActionResult<InitializationResponse>> GetToken ()
        {
            InitializationResponse response = new InitializationResponse();

            try
            {
                response = await _questionService.InitializeQuestions();
            }
            catch (Exception ex)
            {
                response.Error = new DTOs.ErrorDTO()
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message
                };

                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
