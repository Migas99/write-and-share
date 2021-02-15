using System.Collections.Generic;
using System.Threading.Tasks;
using WriteAndShareWebApi.Models.ResponseModels;
using WriteAndShareWebApi.Models.ResponseModels.Notifications;

namespace WriteAndShareWebApi.Interfaces.Services
{
    public interface INotificationService
    {
        Task<List<GetNotificationsResponse>> GetNotifications(string requester);
        Task<List<GetNotificationsResponse>> UpdateNotifications(string requester);
        Task<SuccessResponse> UpdateNotificationById(string requester, int notificationId);
        Task<SuccessResponse> DeleteNotifications(string requester);
        Task<SuccessResponse> DeleteNotificationById(string requester, int notificationId);
    }
}
