using WriteAndShareWebApi.Exceptions;
using WriteAndShareWebApi.Interfaces;
using WriteAndShareWebApi.Models.RequestModels;
using WriteAndShareWebApi.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WriteAndShareWebApi.Controllers
{
    [Produces("application/json")]
    [Route(ApiRoutes.Authentication.Controller)]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService _authenticationService)
        {
            authenticationService = _authenticationService;
        }

        /// <summary>
        /// Regista um utilizador.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     POST /auth/register
        ///     {
        ///        "Username": "TRIPALOSKI",
        ///        "Password": "TRIPALOSKI",
        ///        "Email": "TRIPALOSKI@gmail.com",
        ///        "FirstName": "TRIPA",
        ///        "LastName": "LOSKI",
        ///        "Gender": "Male",
        ///        "BirthDate": "1999-01-30",
        ///        "Telephone": "+391 919283942",
        ///        "Address": "Far Far Away",
        ///        "Privacy": "Public"
        ///     }
        ///
        /// </remarks>
        /// <param name="user"></param>
        /// <returns>Token de autenticação.</returns>
        /// <response code="200">Retorna o token de autenticação.</response>
        /// <response code="400">Retorna se contiver informação inválida.</response>   
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [AllowAnonymous]
        [HttpPost(ApiRoutes.Authentication.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest user)
        {
            try
            {
                return Ok(await authenticationService.Register(user));
            }
            catch (CustomException e)
            {
                return StatusCode(e.GetStatusCode(), new ErrorResponse { Errors = e.GetErrors() });
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, new ErrorResponse());
            }
        }

        /// <summary>
        /// Autentica um utilizador.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     POST /auth/login
        ///     {
        ///        "Username": "TRIPALOSKI",
        ///        "Password": "TRIPALOSKI"
        ///     }
        ///
        /// </remarks>
        /// <param name="user"></param>
        /// <returns>Token de autenticação.</returns>
        /// <response code="200">Retorna o token de autenticação.</response>
        /// <response code="400">Retorna se contiver informação inválida.</response>
        /// <response code="404">Retorna se o username não estiver associado a nenhum utilizador.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [AllowAnonymous]
        [HttpPost(ApiRoutes.Authentication.Authenticate)]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationRequest user)
        {
            try
            {
                return Ok(await authenticationService.Authenticate(user));
            }
            catch (CustomException e)
            {
                return StatusCode(e.GetStatusCode(), new ErrorResponse { Errors = e.GetErrors() });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, new ErrorResponse());
            }
        }
    }
}
