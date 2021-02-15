using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WriteAndShareWebApi.Exceptions;
using WriteAndShareWebApi.Interfaces.Services;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.RequestModels.Comments;
using WriteAndShareWebApi.Models.ResponseModels;
using WriteAndShareWebApi.Utils;

namespace WriteAndShareWebApi.Controllers
{
    [Produces("application/json")]
    [Route(ApiRoutes.Comment.Controller)]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService _commentService)
        {
            commentService = _commentService;
        }

        /// <summary>
        /// Obtêm a lista de comentários feitos por um utilizador.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /users/TRIPALOSKI/comments
        ///
        /// </remarks>
        /// <param name="username"></param>
        /// <returns>Lista de comentários.</returns>
        /// <response code="200">Retorna uma lista de comentários.</response>
        /// <response code="403">Retorna se o utilizador requisitante não tiver permissões para aceder a esta informação.</response>
        /// <response code="404">Retorna se o username não estiver associado a um utilizador.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response> 
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Comment.GetCommentsByUser)]
        public async Task<IActionResult> GetCommentsByUser(string username)
        {
            try
            {
                return Ok(await commentService.GetCommentsByUser(JwtHandler.GetUsername(User), JwtHandler.GetUserRole(User), username));
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
        /// Obtêm a lista de comentários feitos a uma publicação.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /posts/1/comments
        ///
        /// </remarks>
        /// <param name="postId"></param>
        /// <returns>Lista de comentários.</returns>
        /// <response code="200">Retorna uma lista de comentários.</response>
        /// <response code="404">Retorna se o id não estiver associado a uma publicação.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response> 
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Comment.GetCommentsByPublication)]
        public async Task<IActionResult> GetCommentsByPublication(int postId)
        {
            try
            {
                return Ok(await commentService.GetCommentsByPublication(JwtHandler.GetUsername(User), JwtHandler.GetUserRole(User), postId));
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
        /// Obtêm a lista de comentários feitos a um comentário.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /comments/1/comments
        ///
        /// </remarks>
        /// <param name="commentId"></param>
        /// <returns>Lista de comentários.</returns>
        /// <response code="200">Retorna uma lista de comentários.</response>
        /// <response code="404">Retorna se o id não estiver associado a um comentário.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response> 
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Comment.GetCommentsByComment)]
        public async Task<IActionResult> GetCommentsByComment(int commentId)
        {
            try
            {
                return Ok(await commentService.GetCommentsByComment(JwtHandler.GetUsername(User), JwtHandler.GetUserRole(User), commentId));
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
        /// Obtêm a lista de comentários feitos pelo utilizador requisitante.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /comments/mycomments
        ///
        /// </remarks>
        /// <returns>Lista de comentários.</returns>
        /// <response code="200">Retorna a lista de comentários.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response> 
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Comment.GetMyComments)]
        public async Task<IActionResult> GetMyComments()
        {
            try
            {
                return Ok(await commentService.GetMyComments(JwtHandler.GetUsername(User)));
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
        /// Obtêm um comentário.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /comments/1
        ///
        /// </remarks>
        /// <param name="commentId"></param>
        /// <returns>Um comentários.</returns>
        /// <response code="200">Retorna um comentário.</response>
        /// <response code="403">Retorna se o utilizador requisitante não tiver permissões para aceder a este comentário.</response>
        /// <response code="404">Retorna se o id não estiver associado a um comentário.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response> 
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Comment.GetCommentById)]
        public async Task<IActionResult> GetCommentById(int commentId)
        {
            try
            {
                return Ok(await commentService.GetCommentById(JwtHandler.GetUsername(User), JwtHandler.GetUserRole(User), commentId));
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
        /// Cria um novo comentário.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     POST /comments/new
        ///     {
        ///        "Target": "1",
        ///        "Message": "Mensagem do comentário",
        ///        "Mentions": ["TRIPALOSKI", "TRIPALOSKI1"]
        ///     }
        ///
        /// </remarks>
        /// <param name="req"></param>
        /// <returns>O comentário criado.</returns>
        /// <response code="200">Retorna o comentário criado.</response>
        /// <response code="403">Retorna se o utilizador requisitante não tiver permissões para comentar a publicação ou comentário alvo.</response>
        /// <response code="404">Retorna se a publicação ou comentário alvo não existirem ou se um dos utilizadores mencionados pelo utilizador requisitante
        /// não existe.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response> 
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpPost(ApiRoutes.Comment.CreateComment)]
        public async Task<IActionResult> AddComment([FromBody] CreateCommentRequest req)
        {
            try
            {
                return Ok(await commentService.AddComment(JwtHandler.GetUsername(User), req));
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
        /// Elimina o conteúdo de um comentário.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     DELETE /comments/1
        ///
        /// </remarks>
        /// <param name="commentId"></param>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">Retorna uma mensagem de sucesso.</response>
        /// <response code="403">Retorna se o utilizador requisitante estiver a tentar eliminar um comentário de um outro utilizador.</response>
        /// <response code="404">Retorna se o id não estiver associado a um comentário.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response> 
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpDelete(ApiRoutes.Comment.DeleteComment)]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            try
            {
                return Ok(await commentService.DeleteComment(JwtHandler.GetUsername(User), commentId));
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
