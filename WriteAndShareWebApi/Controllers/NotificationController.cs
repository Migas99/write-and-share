using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WriteAndShareWebApi.Exceptions;
using WriteAndShareWebApi.Interfaces.Services;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.ResponseModels;
using WriteAndShareWebApi.Utils;

namespace WriteAndShareWebApi.Controllers
{
    [Produces("application/json")]
    [Route(ApiRoutes.Notifications.Controller)]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService _notificationService)
        {
            notificationService = _notificationService;
        }

        /// <summary>
        /// Obtêm a lista de notificações do utilizador requisitante.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     GET /notifications
        ///
        /// </remarks>
        /// <returns>Lista de notificações do utilizador requisitante.</returns>
        /// <response code="200">Retorna a lista de notificações do utilizador requisitante.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpGet(ApiRoutes.Notifications.GetMyNotifications)]
        public async Task<IActionResult> GetMyNotifications()
        {
            try
            {
                return Ok(await notificationService.GetNotifications(JwtHandler.GetUsername(User)));
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
        /// Atualiza o estado de todas as notificações do utilizador para "vistas".
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     PUT /notifications
        ///
        /// </remarks>
        /// <returns>Lista de notificações com o estado atualizado.</returns>
        /// <response code="200">Retorna a lista de notificações com o estado atualizado.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpPut(ApiRoutes.Notifications.UpdateMyNotifications)]
        public async Task<IActionResult> UpdateMyNotifications()
        {
            try
            {
                return Ok(await notificationService.UpdateNotifications(JwtHandler.GetUsername(User)));
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
        /// Atualiza o estado de uma notificação para vista.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     PUT /notifications/1
        ///
        /// </remarks>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">Retorna uma mensagem de sucesso.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpPut(ApiRoutes.Notifications.UpdateMyNotification)]
        public async Task<IActionResult> UpdateMyNotification(int notificationId)
        {
            try
            {
                return Ok(await notificationService.UpdateNotificationById(JwtHandler.GetUsername(User), notificationId));
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
        /// Elimina todas as notificações do utilizador requisitante.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     DELETE /notifications
        ///
        /// </remarks>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">Retorna uma mensagem de sucesso.</response>
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpDelete(ApiRoutes.Notifications.DeleteMyNotifications)]
        public async Task<IActionResult> DeleteMyNotifications()
        {
            try
            {
                return Ok(await notificationService.DeleteNotifications(JwtHandler.GetUsername(User)));
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
        /// Elimina uma notificação dado o seu id.
        /// </summary>
        /// <remarks>
        /// Exemplo de um pedido:
        ///
        ///     DELETE /notifications/1
        ///
        /// </remarks>
        /// <param name="notificationId"></param>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">Retorna uma mensagem de sucesso.</response>
        /// <response code="403">Retorna se o utilizador requisitante estiver a tentar eliminar uma notificação que não é sua.</response>
        /// <response code="404">Retorna se o id fornecido não estiver associado a uma notificação.</response> 
        /// <response code="500">Retorna se ocorrer algum problema interno no servidor.</response>   
        [Authorize(Roles = Roles.Administrator + "," + Roles.User)]
        [HttpDelete(ApiRoutes.Notifications.DeleteNotificationById)]
        public async Task<IActionResult> DeleteNotificationById(int notificationId)
        {
            try
            {
                return Ok(await notificationService.DeleteNotificationById(JwtHandler.GetUsername(User), notificationId));
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
