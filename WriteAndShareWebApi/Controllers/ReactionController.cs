using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WriteAndShareWebApi.Exceptions;
using WriteAndShareWebApi.Interfaces.Services;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.ResponseModels;
using WriteAndShareWebApi.Utils;

namespace WriteAndShareWebApi.Controllers
{
    [Produces("application/json")]
    [Route(ApiRoutes.Reactions.Controller)]
    [ApiController]
    public class ReactionController : Controller
    {
        private readonly IReactionService reactionService;

        public ReactionController(IReactionService _reactionService)
        {
            reactionService = _reactionService;
        }

        /// <summary>
        /// Obtêm a lista de publicações nas quais o utilizador requisitante reagiu.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /myreaction/posts
        ///
        /// </remarks>
        /// <returns>Lista de publicações nas quais o utilizador requisitante reagiu.</returns>
        /// <response code="200">Retorna uma lista de publicações nas quais o utilizador requisitante reagiu.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Reactions.GetMyReactionsToPosts)]
        public async Task<IActionResult> GetMyReactionsToPosts()
        {
            try
            {
                return Ok(await reactionService.GetPublicationsReactedByRequester(JwtHandler.GetUsername(User)));
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
        /// Obtêm a lista de publicações nas quais um utilizador reagiu.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /reaction/TRIPALOSKI/posts
        ///
        /// </remarks>
        /// <returns>Lista de publicações nas quais um utilizador reagiu.</returns>
        /// <response code="200">Retorna uma lista de publicações nas quais um utilizador reagiu.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Reactions.GetReactionsOfUserToPosts)]
        public async Task<IActionResult> GetPostsReactedByuser(string username)
        {
            try
            {
                return Ok(await reactionService.GetPublicationsReactedByUser(JwtHandler.GetUsername(User), JwtHandler.GetUserRole(User), username));
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
        /// Obtêm a lista de comentários nas quais o utilizador requisitante reagiu.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /myreaction/comments
        ///
        /// </remarks>
        /// <returns>Lista de comentários nas quais o utilizador requisitante reagiu.</returns>
        /// <response code="200">Retorna uma lista de comentários nas quais o utilizador requisitante reagiu.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Reactions.GetMyReactionsToComments)]
        public async Task<IActionResult> GetMyReactionsToComments()
        {
            try
            {
                return Ok(await reactionService.GetCommentsReactedByRequester(JwtHandler.GetUsername(User)));
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
        /// Obtêm a lista de comentários nas quais um utilizador reagiu.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /reaction/TRIPALOSKI/comments
        ///
        /// </remarks>
        /// <returns>Lista de comentários nas quais um utilizador reagiu.</returns>
        /// <response code="200">Retorna uma lista de comentários nas quais um utilizador reagiu.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Reactions.GetReactionsOfUserToComments)]
        public async Task<IActionResult> GetCommentsReactedByuser(string username)
        {
            try
            {
                return Ok(await reactionService.GetCommentsReactedByUser(JwtHandler.GetUsername(User), JwtHandler.GetUserRole(User), username));
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
        /// Dá upvote numa publicação ou comentário.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     POST /reaction/1/upvote
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">Retorna uma mensagem de sucesso.</response>
        /// <response code="403">Retorna se o utilizador não tiver permissões para reagir a esta publicação ou comentário.</response>
        /// <response code="404">Retorna se o id não estiver associado a uma publicação ou comentário.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpPost(ApiRoutes.Reactions.Upvote)]
        public async Task<IActionResult> Upvote(int id)
        {
            try
            {
                return Ok(await reactionService.Upvote(JwtHandler.GetUsername(User), id));
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
        /// Dá downvote numa publicação ou comentário.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     POST /reaction/1/downvote
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">Retorna uma mensagem de sucesso.</response>
        /// <response code="403">Retorna se o utilizador não tiver permissões para reagir a esta publicação ou comentário.</response>
        /// <response code="404">Retorna se o id não estiver associado a uma publicação ou comentário.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpPost(ApiRoutes.Reactions.Downvote)]
        public async Task<IActionResult> Downvote(int id)
        {
            try
            {
                return Ok(await reactionService.Downvote(JwtHandler.GetUsername(User), id));
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
        /// Remove a reação a uma publicação ou comentário
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     DELETE /reaction/1
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">Retorna uma mensagem de sucesso.</response>
        /// <response code="400">Retorna se o utilizador não tiver sequer reagido a esta publicação ou comentário em primeiro lugar.</response>
        /// <response code="404">Retorna se o id não estiver associado a uma publicação ou comentário.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpDelete(ApiRoutes.Reactions.DeleteVote)]
        public async Task<IActionResult> DeleteVote(int id)
        {
            try
            {
                return Ok(await reactionService.DeleteVote(JwtHandler.GetUsername(User), id));
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
