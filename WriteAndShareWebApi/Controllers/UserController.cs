using WriteAndShareWebApi.Exceptions;
using WriteAndShareWebApi.Interfaces;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.RequestModels;
using WriteAndShareWebApi.Models.ResponseModels;
using WriteAndShareWebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WriteAndShareWebApi.Controllers
{
    [Produces("application/json")]
    [Route(ApiRoutes.User.Controller)]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        /// <summary>
        /// Obtêm uma lista que contêm a informação básica de todos os utilizadores.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /users
        ///
        /// </remarks>
        /// <returns>Lista que contêm a informação básica de todos os utilizadores.</returns>
        /// <response code="200">Retorna a lista que contêm a informação básica de todos os utilizadores.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.User.GetAllUsersBasicInfo)]
        public async Task<IActionResult> GetAllUsersBasicInfo()
        {
            try
            {
                return Ok(await userService.GetAllUsersBasicInfo(JwtHandler.GetUsername(User)));
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

        /// <summary>
        /// Obtêm uma lista que contêm a informação básica dos utilizadores requisitados.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     POST /users
        ///     {
        ///        usernames: ["TRIPALOSKI","TRIAPLOSKI2"]
        ///     }
        ///
        /// </remarks>
        /// <returns>Lista que contêm a informação básica de todos os utilizadores requisitados.</returns>
        /// <response code="200">Retorna a lista que contêm a informação básica de todos os utilizadores requisitados.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpPost(ApiRoutes.User.GetListOfUsersBasicInfoByUsernames)]
        public async Task<IActionResult> GetListOfUsersBasicInfo([FromBody] List<string> usernames)
        {
            try
            {
                return Ok(await userService.GetListOfUsersBasicInfoByUsername(JwtHandler.GetUsername(User), usernames));
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

        /// <summary>
        /// Obtêm a informação de um utilizador, dado o seu username.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /users/TRIPALOSKI
        ///
        /// </remarks>
        /// <param name="username"></param>
        /// <returns>Retorna a informação do utilizador.</returns>
        /// <response code="200">Retorna a informação do utilizador.</response>
        /// <response code="403">Retorna se o requisitante não tiver permissões para obter a informação deste utilizador.</response>
        /// <response code="404">Retorna se o username não estiver associado a nenhum utilizador.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.User.GetUserByUsername)]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            try
            {
                return Ok(await userService.GetUserByUsername(JwtHandler.GetUsername(User), JwtHandler.GetUserRole(User), username));
            }
            catch (CustomException e)
            {
                if(e.GetStatusCode() == 403)
                {
                    return StatusCode(e.GetStatusCode(), await userService.GetUserBasicInfo(JwtHandler.GetUsername(User), username));
                }
                else
                {
                    return StatusCode(e.GetStatusCode(), new ErrorResponse { Errors = e.GetErrors() });
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, new ErrorResponse());
            }

        }

        /// <summary>
        /// Obtêm as informações do utilizador requisitante.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /users/profile
        ///
        /// </remarks>
        /// <returns>Token de autenticação.</returns>
        /// <response code="200">Retorna a informação do utilizador requisitante.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.User.GetProfile)]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                return Ok(await userService.GetProfile(JwtHandler.GetUsername(User)));
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

        /// <summary>
        /// Atualiza o Header do utilizador requisitante.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido (o pedido têm de ser um form/data):
        ///
        ///     PUT /users/header
        ///     {
        ///        "header": "Novo header"
        ///     }
        ///
        /// </remarks>
        /// <param name="header"></param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Retorna uma mensagem de sucesso.</response>
        /// <response code="400">Retorna se o formato do ficheiro não for válido.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpPut(ApiRoutes.User.UpdateHeader)]
        public async Task<IActionResult> UpdateHeader(IFormFile header)
        {
            try
            {
                return Ok(await userService.UpdateHeader(JwtHandler.GetUsername(User), header));
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

        /// <summary>
        /// Atualiza o avatar do utilizador requisitante.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido (o pedido têm de ser um form/data):
        ///
        ///     PUT /users/avatar
        ///     {
        ///        "avatar": "O novo avatar"
        ///     }
        ///
        /// </remarks>
        /// <param name="avatar"></param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Retorna uma mensagem de sucesso.</response>
        /// <response code="400">Retorna se o formato do ficheiro não for válido.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpPut(ApiRoutes.User.UpdateAvatar)]
        public async Task<IActionResult> UpdateAvatar(IFormFile avatar)
        {
            try
            {
                return Ok(await userService.UpdateAvatar(JwtHandler.GetUsername(User), avatar));
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

        /// <summary>
        /// Atualiza algumas das informações do utilizador requisitante.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     PUT /users/profile
        ///     {
        ///        "CurrentPassword": "TRIPALOSKI",
        ///        "NewPassword": "TRIPALOSKI",
        ///        "Email": "TRIPALOSKI_NEW@gmail.com",
        ///        "Telephone": "+391 923922222",
        ///        "Address": "Not Far Far Away",
        ///        "Privacy": "Private"
        ///     }
        ///
        /// </remarks>
        /// <param name="update"></param>
        /// <returns>O utilizador atualizado.</returns>
        /// <response code="200">Retorna as informações do utilizador atualizadas.</response>
        /// <response code="400">Retorna se contiver informação inválida.</response>
        /// <response code="403">Retorna se a password estiver errada.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpPut(ApiRoutes.User.UpdateProfile)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest update)
        {
            try
            {
                return Ok(await userService.UpdateUser(JwtHandler.GetUsername(User), update));
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

        /// <summary>
        /// Elimina toda a informação relativa ao utilizador requisitante.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     DELETE /users/profile
        ///
        /// </remarks>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">Retorna a mensagem de sucesso.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpDelete(ApiRoutes.User.DeleteProfile)]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                return Ok(await userService.DeleteUser(JwtHandler.GetUsername(User)));
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
