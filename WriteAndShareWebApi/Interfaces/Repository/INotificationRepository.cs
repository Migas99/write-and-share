using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.ResponseModels;

namespace WriteAndShareWebApi.Interfaces.Repository
{
    public interface INotificationRepository
    {
        Task<Notification> AddNotification(Notification notification);
        Task<List<Notification>> GetNotificationsByUser(string username);
        Task<Notification> GetNotificationById(int notificationId);
        Task<List<Notification>> UpdateNotificationsByUser(string username);
        Task<Notification> UpdateNotificationById(int notificationId);
        Task<SuccessResponse> DeleteNotificationsByUser(string username);
        Task<SuccessResponse> DeleteNotificationById(int notificationId);
    }
}
