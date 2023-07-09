using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shape.Requests;
using Shape.Rsponses;
using Shape.Services.Interface;

namespace Shape.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;


        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        /// <summary>
        /// </summary>
        /// <response code="200">If all working fine</response>
        /// <response code="200">Invalid Input</response>
        /// <response code="500">If Server Error</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Rsponses.Response<bool>), 200)]
        [ProducesResponseType(typeof(Rsponses.Response<bool>), 400)]
        [HttpPost]

        [Route("signup")]
        public async Task<IActionResult> Signup(SignupRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState.Values.SelectMany(v => v.Errors.Select(z => z.ErrorMessage)));
                }

                var response = await accountService.Signup(request);

                if (response.Item1)
                {         
                    return StatusCode(StatusCodes.Status200OK, new Rsponses.Response<bool>() { isError = false, messages = response.Item2, data = response.Item1 });
                }
                return StatusCode(StatusCodes.Status400BadRequest, new Rsponses.Response<bool>() { isError = true, messages = response.Item2, data = response.Item1 });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
