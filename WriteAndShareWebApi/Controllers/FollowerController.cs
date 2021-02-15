using WriteAndShareWebApi.Exceptions;
using WriteAndShareWebApi.Interfaces.Services;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.ResponseModels;
using WriteAndShareWebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WriteAndShareWebApi.Controllers
{
    [Produces("application/json")]
    [Route(ApiRoutes.Follower.Controller)]
    [ApiController]
    public class FollowerController : Controller
    {
        private readonly IFollowerService followerService;

        public FollowerController(IFollowerService _followerService)
        {
            followerService = _followerService;
        }


        /// <summary>
        /// Obtêm a lista de utilizadores que seguem o utilizador requisitante.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /following
        ///
        /// </remarks>
        /// <returns>Lista de utilizadores que seguem o utilizador requisitante.</returns>
        /// <response code="200">Retorna a lista de utilizadores que seguem o utilizador requisitante.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>  
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Follower.GetFollowers)]
        public async Task<IActionResult> GetFollowers()
        {
            try
            {
                return Ok(await followerService.GetFollowers(JwtHandler.GetUsername(User)));
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
        /// Obtêm a lista de utilizadores que o utilizador requisitante segue.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /followers
        ///
        /// </remarks>
        /// <returns>Lista de utilizadores que o utilizador requisitante segue.</returns>
        /// <response code="200">Retorna a lista de utilizadores que o utilizador requisitante segue.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>    
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Follower.GetFollowing)]
        public async Task<IActionResult> GetFollowing()
        {
            try
            {
                return Ok(await followerService.GetFollowing(JwtHandler.GetUsername(User)));
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
        /// Obtêm a lista de pedidos feitos para seguir o utilizador requisitante.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /requests
        ///
        /// </remarks>
        /// <returns>Lista de pedidos feitos para seguir o utilizador requisitante.</returns>
        /// <response code="200">Retorna a lista de pedidos feitos para seguir o utilizador requisitante.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Follower.GetRequests)]
        public async Task<IActionResult> GetRequests()
        {
            try
            {
                return Ok(await followerService.GetRequestsReceived(JwtHandler.GetUsername(User)));
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
        /// Obtêm a lista de pedidos feitos pelo utilizador requisitante para seguir outros utilizadores.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /requested
        ///
        /// </remarks>
        /// <returns>Lista de pedidos feitos pelo utilizador requisitante para seguir outros utilizadores.</returns>
        /// <response code="200">Retorna a lista de pedidos feitos pelo utilizador requisitante para seguir outros utilizadores.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Follower.GetRequested)]
        public async Task<IActionResult> GetRequested()
        {
            try
            {
                return Ok(await followerService.GetRequestsMade(JwtHandler.GetUsername(User)));
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
        /// Obtêm a lista de seguidores de um utilizador.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /followers/TRIPALOSKI
        ///
        /// </remarks>
        /// <param name="username"></param>
        /// <returns>Lista de seguidores de um utilizador.</returns>
        /// <response code="200">Retorna a lista de seguidores de um utilizador.</response>
        /// <response code="403">Retorna se o utilizador requisitante não tiver permissões para aceder a esta informação.</response>
        /// <response code="404">Retorna se o username não estiver associado a nenhum utilizador.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Follower.GetUserFollowers)]
        public async Task<IActionResult> GetUserFollowers(string username)
        {
            try
            {
                return Ok(await followerService.GetUserFollowers(JwtHandler.GetUsername(User), JwtHandler.GetUserRole(User), username));
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
        /// Obtêm a lista de utilizadores que um utilizador segue.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /following/TRIPALOSKI
        ///
        /// </remarks>
        /// <param name="username"></param>
        /// <returns>Lista de utilizadores que um utilizador segue.</returns>
        /// <response code="200">Retorna a lista de utilizadores que um utilizador segue.</response>
        /// <response code="403">Retorna se o utilizador requisitante não tiver permissões para aceder a esta informação.</response>
        /// <response code="404">Retorna se o username não estiver associado a nenhum utilizador.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Follower.GetUserFollowing)]
        public async Task<IActionResult> GetUserFollowing(string username)
        {
            try
            {
                return Ok(await followerService.GetUserFollowing(JwtHandler.GetUsername(User), JwtHandler.GetUserRole(User), username));
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
        /// Segue um utilizador.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     POST /follow/TRIPALOSKI
        ///
        /// </remarks>
        /// <param name="username"></param>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">Retorna uma mensagem de sucesso. Caso a privacidade do utilizador alvo seja pública, o utilizador requisitante irá o seguir
        /// imediatamente. Caso seja privada, será enviado um pedido para que o utilizador requisitante o siga.</response>
        /// <response code="400">Retorna se o utilizador requisitante estiver a tentar seguir a si mesmo ou se já seguir o outro utilizador.</response>
        /// <response code="404">Retorna se o username não estiver associado a nenhum utilizador.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpPost(ApiRoutes.Follower.Follow)]
        public async Task<IActionResult> FollowUser(string username)
        {
            try
            {
                return Ok(await followerService.FollowUser(JwtHandler.GetUsername(User), username));
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
        /// Deixa de seguir um utilizador.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     DELETE /unfollow/TRIPALOSKI
        ///
        /// </remarks>
        /// <param name="username"></param>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">Retorna uma mensagem de sucesso.</response>
        /// <response code="400">Retorna se o utilizador requisitante estiver a tentar deixar de seguir a si mesmo ou um outro utilizador que não o segue.</response>
        /// <response code="404">Retorna se o username não estiver associado a nenhum utilizador.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpDelete(ApiRoutes.Follower.Unfollow)]
        public async Task<IActionResult> UnfollowUser(string username)
        {
            try
            {
                return Ok(await followerService.UnfollowUser(JwtHandler.GetUsername(User), username));
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
        /// Remove um utilizador da lista de seguidores do utilizador requisitante.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     DELETE /unfollower/TRIPALOSKI
        ///
        /// </remarks>
        /// <param name="username"></param>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">Retorna uma mensagem de sucesso.</response>
        /// <response code="400">Retorna se o utilizador requisitante estiver a tentar remover a si mesmo como seu seguidor ou um outro utilizador que não o segue.</response>
        /// <response code="404">Retorna se o username não estiver associado a nenhum utilizador.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpDelete(ApiRoutes.Follower.RemoveFollower)]
        public async Task<IActionResult> RemoveUserAsFollower(string username)
        {
            try
            {
                return Ok(await followerService.RemoveUserAsFollower(JwtHandler.GetUsername(User), username));
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
        /// Cancela um pedido feito pelo utilizador requisitante.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     DELETE /1/cancel
        ///
        /// </remarks>
        /// <param name="requestId"></param>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">Retorna uma mensagem de sucesso.</response>
        /// <response code="404">Retorna se o id do pedido não estiver associado a um pedido feito pelo utilizador.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpDelete(ApiRoutes.Follower.CancelRequest)]
        public async Task<IActionResult> CancelRequest(int requestId)
        {
            try
            {
                return Ok(await followerService.CancelRequest(JwtHandler.GetUsername(User), requestId));
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
        /// Aceita um pedido.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /1/accept
        ///
        /// </remarks>
        /// <param name="requestId"></param>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">Retorna uma mensagem de sucesso.</response>
        /// <response code="404">Retorna se o pedido não tiver sido encontrado.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpPut(ApiRoutes.Follower.AcceptRequest)]
        public async Task<IActionResult> AcceptRequest(int requestId)
        {
            try
            {
                return Ok(await followerService.AcceptRequest(JwtHandler.GetUsername(User), requestId));
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
        /// Recusa um pedido.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     DELETE /1/refuse
        ///
        /// </remarks>
        /// <param name="requestId"></param>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">Retorna uma mensagem de sucesso.</response>
        /// <response code="404">Retorna se o id do pedido não estiver associado a um pedido feito para o utilizador.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpDelete(ApiRoutes.Follower.RefuseRequest)]
        public async Task<IActionResult> RefuseRequest(int requestId)
        {
            try
            {
                return Ok(await followerService.RefuseRequest(JwtHandler.GetUsername(User), requestId));
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
