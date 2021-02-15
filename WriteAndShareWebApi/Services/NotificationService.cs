using System.Collections.Generic;
using System.Threading.Tasks;
using WriteAndShareWebApi.Exceptions;
using WriteAndShareWebApi.Interfaces.Repository;
using WriteAndShareWebApi.Interfaces.Services;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.ResponseModels;
using WriteAndShareWebApi.Models.ResponseModels.Notifications;

namespace WriteAndShareWebApi.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository notificationRepository;

        public NotificationService(INotificationRepository _notificationRepository)
        {
            notificationRepository = _notificationRepository;
        }

        public async Task<List<GetNotificationsResponse>> GetNotifications(string requester)
        {
            List<GetNotificationsResponse> res = new List<GetNotificationsResponse>();
            List<Notification> notifications = await notificationRepository.GetNotificationsByUser(requester);

            foreach(Notification notification in notifications)
            {
                res.Add(new GetNotificationsResponse
                {
                    Id = notification.Id,
                    Type = notification.Type,
                    Author = notification.Author,
                    Target = notification.Target,
                    PostId = notification.PostId,
                    CommentId = notification.CommentId,
                    AnswerId = notification.AnswerId,
                    SubmitDate = notification.SubmitDate,
                    AlreadySeen = notification.AlreadySeen
                });
            }

            return res;
        }

        public async Task<List<GetNotificationsResponse>> UpdateNotifications(string requester)
        {
            List<GetNotificationsResponse> res = new List<GetNotificationsResponse>();
            List<Notification> notifications = await notificationRepository.UpdateNotificationsByUser(requester);

            foreach (Notification notification in notifications)
            {
                res.Add(new GetNotificationsResponse
                {
                    Id = notification.Id,
                    Type = notification.Type,
                    Author = notification.Author,
                    Target = notification.Target,
                    PostId = notification.PostId,
                    CommentId = notification.CommentId,
                    AnswerId = notification.AnswerId,
                    SubmitDate = notification.SubmitDate,
                    AlreadySeen = notification.AlreadySeen
                });
            }

            return res;
        }

        public async Task<SuccessResponse> UpdateNotificationById(string requester, int notificationId)
        {
            Notification notification = await notificationRepository.GetNotificationById(notificationId);
            if (notification.Target != requester) throw new CustomException(403, "You don't have permissions to access this notification.");
            await notificationRepository.UpdateNotificationById(notificationId);
            return new SuccessResponse { Success = "The notification was seen." };
        }

        public async Task<SuccessResponse> DeleteNotifications(string requester)
        {
            return await notificationRepository.DeleteNotificationsByUser(requester);
        }

        public async Task<SuccessResponse> DeleteNotificationById(string requester, int notificationId)
        {
            Notification notification = await notificationRepository.GetNotificationById(notificationId);
            if (notification == null) throw new CustomException(404, "The notification was not found.");
            if (notification.Target != requester) throw new CustomException(403, "You can't delete this notification.");
            return await notificationRepository.DeleteNotificationById(notificationId);
        }
    }
}
