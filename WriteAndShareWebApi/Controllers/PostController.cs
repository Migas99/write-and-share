using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WriteAndShareWebApi.Exceptions;
using WriteAndShareWebApi.Interfaces.Services;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.RequestModels;
using WriteAndShareWebApi.Models.ResponseModels;
using WriteAndShareWebApi.Utils;

namespace WriteAndShareWebApi.Controllers
{
    [Produces("application/json")]
    [Route(ApiRoutes.Post.Controller)]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService _postService)
        {
            postService = _postService;
        }

        /// <summary>
        /// Obtêm uma lista de publicações feitas pelos utilizadores que o utilizador requisitante segue, por ordem da data de publicação.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /posts/feed
        ///
        /// </remarks>
        /// <returns>Lista de publicações feitas pelos utilizadores que o utilizador requisitante segue, por ordem da data de publicação.</returns>
        /// <response code="200">Retorna a lista de publicações feitas pelos utilizadores que o utilizador requisitante segue, por ordem da data de publicação.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Post.Feed)]
        public async Task<IActionResult> GetFeed()
        {
            try
            {
                return Ok(await postService.ObtainFeed(JwtHandler.GetUsername(User)));
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
        /// Obtêm uma lista de publicações que o utilizador requisitante pode visualizar, por ordem da data de publicação.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /posts
        ///
        /// </remarks>
        /// <returns>Lista de publicações que o utilizador requisitante pode visualizar, por ordem da data de publicação.</returns>
        /// <response code="200">Retorna a lista de publicações que o utilizador requisitante pode visualizar, por ordem da data de publicação.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Post.GetAllPosts)]
        public async Task<IActionResult> GetAllPosts()
        {
            try
            {
                return Ok(await postService.GetAllPosts(JwtHandler.GetUsername(User), JwtHandler.GetUserRole(User)));
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
        /// Obtêm uma lista de publicações feitas por um utilizador, por ordem da data de publicação.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /posts/TRIPALOSKI
        ///
        /// </remarks>
        /// <returns>Lista de publicações feitas por um utilizador, por ordem da data de publicação.</returns>
        /// <response code="200">Retorna a lista de publicações feitas por um utilizador, por ordem da data de publicação.</response>
        /// <response code="403">Retorna se o utilizador requisitante não tiver permissões para visualizar esta informação.</response>
        /// <response code="404">Retorna se o username não estiver associado a um utilizador.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Post.GetAllPostsByUsername)]
        public async Task<IActionResult> GetPostsByUser(string username)
        {
            try
            {
                return Ok(await postService.GetPostsByUsername(JwtHandler.GetUsername(User), JwtHandler.GetUserRole(User), username));
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
        /// Obtêm uma lista de publicações realizadas pelo utilizador requisitante, por ordem da data de publicação.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /posts/myposts
        ///
        /// </remarks>
        /// <returns>Lista de publicações realizadas pelo utilizador requisitante, por ordem da data de publicação.</returns>
        /// <response code="200">Retorna a lista de publicações realizadas pelo utilizador requisitante, por ordem da data de publicação.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Post.GetMyPosts)]
        public async Task<IActionResult> GetMyPosts()
        {
            try
            {
                return Ok(await postService.GetMyPosts(JwtHandler.GetUsername(User)));
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
        /// Obtêm uma lista de publicações onde o utilizador requisitante foi mencionado, por ordem da data de publicação.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /posts/postsmentioned
        ///
        /// </remarks>
        /// <returns>Lista de publicações onde o utilizador requisitante foi mencionado, por ordem da data de publicação.</returns>
        /// <response code="200">Retorna a lista de publicações onde o utilizador requisitante foi mencionado, por ordem da data de publicação.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Post.GetPostsMentioned)]
        public async Task<IActionResult> GetPostsMentioned()
        {
            try
            {
                return Ok(await postService.GetPostsMentioned(JwtHandler.GetUsername(User)));
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
        /// Obtêm uma publicação.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /posts/post/1
        ///
        /// </remarks>
        /// <returns>Uma publicação.</returns>
        /// <response code="200">Retorna uma publicação.</response>
        /// <response code="403">Retorna se o utilizador requisitante não tiver permissões para aceder a esta publicação.</response>
        /// <response code="404">Retorna se o id não estiver associado a uma publicação.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Post.GetPostById)]
        public async Task<IActionResult> GetPostById(int postId)
        {
            try
            {
                return Ok(await postService.GetPostById(JwtHandler.GetUsername(User), JwtHandler.GetUserRole(User), postId));
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
        /// Cria uma nova publicação (máximo 50MB).
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido (o pedido têm de ser feito em form/data em https):
        ///
        ///     POST /posts/new
        ///     {
        ///        "Upload": "Um possível upload efetuado pelo utilizador",
        ///        "Link": "Um possível link associado a uma imagem ou vídeo hospedado em terceiros",
        ///        "Message": "Mensagem do utilizador associada à publicação",
        ///        "Mentions": "TRIPALOSKI;TRIPALOSKI1;TRIPALOSKI2;..."
        ///     }
        ///
        /// </remarks>
        /// <param name="post"></param>
        /// <returns>A publicação criada.</returns>
        /// <response code="200">Retorna a publicação criada.</response>
        /// <response code="404">Retorna se um dos utilizadores mencionados não existir.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpPost(ApiRoutes.Post.CreatePost)]
        [RequestSizeLimit(52428800)]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostRequest post)
        {
            try
            {
                return Ok(await postService.CreatePost(JwtHandler.GetUsername(User), post));
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
        /// Elimina o conteúdo de uma publicação.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     DELETE /posts/post/1
        ///
        /// </remarks>
        /// <param name="postId"></param>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">Retorna uma mensagem de sucesso.</response>
        /// <response code="403">Retorna se o utilizador requisitante não tiver permissões para eliminar esta publicação.</response>
        /// <response code="404">Retorna se o id não estiver associado a uma publicação.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpDelete(ApiRoutes.Post.DeletePost)]
        public async Task<IActionResult> DeletePost(int postId)
        {
            try
            {
                return Ok(await postService.DeletePost(JwtHandler.GetUsername(User), postId));
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
