using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WriteAndShareWebApi.Interfaces.Repository;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.ResponseModels;
using static WriteAndShareWebApi.Neo4j.CypherQueries.NotificationQueries;

namespace WriteAndShareWebApi.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IDriver driver;

        public NotificationRepository(IDriver _driver)
        {
            driver = _driver;
        }

        public async Task<Notification> AddNotification(Notification notification)
        {
            IAsyncSession session = driver.AsyncSession();
            Notification res = null;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        CreateNotificationQuery(),
                        new Dictionary<string, object> {
                            { "Type", notification.Type },
                            { "Author", notification.Author },
                            { "Target", notification.Target },
                            { "SubmitDate", DateTime.Now },
                            { "AlreadySeen", false }
                        });

                    while (await cursor.FetchAsync())
                    {
                        res = new Notification
                        {
                            Id = cursor.Current["Id"].As<int>(),
                            Type = cursor.Current["Type"].As<string>(),
                            Author = cursor.Current["Author"].As<string>(),
                            Target = cursor.Current["Target"].As<string>(),
                            SubmitDate = cursor.Current["SubmitDate"].As<DateTime>(),
                            AlreadySeen = cursor.Current["AlreadySeen"].As<bool>()
                        };
                    }

                    res.PostId = null;
                    if (notification.PostId != null)
                    {
                        cursor = await tx.RunAsync(
                        RelatedNotificationToPostQuery(),
                        new Dictionary<string, object> {
                            { "NotificationId", res.Id },
                            { "PostId", int.Parse(notification.PostId) }
                        });

                        while (await cursor.FetchAsync())
                        {
                            res = new Notification
                            {
                                PostId = cursor.Current["Id"].As<string>()
                            };
                        }
                    }

                    res.CommentId = null;
                    if(notification.CommentId != null)
                    {
                        cursor = await tx.RunAsync(
                        RelatedNotificationToCommentQuery(),
                        new Dictionary<string, object> {
                            { "NotificationId", res.Id },
                            { "CommentId", int.Parse(notification.CommentId) }
                        });

                        while (await cursor.FetchAsync())
                        {
                            res = new Notification
                            {
                                CommentId = cursor.Current["Id"].As<string>()
                            };
                        }
                    }

                    res.AnswerId = null;
                    if(notification.AnswerId != null)
                    {
                        cursor = await tx.RunAsync(
                        RelatedNotificationToAnswerQuery(),
                        new Dictionary<string, object> {
                            { "NotificationId", res.Id },
                            { "CommentId", int.Parse(notification.AnswerId) }
                        });

                        while (await cursor.FetchAsync())
                        {
                            res = new Notification
                            {
                                AnswerId = cursor.Current["Id"].As<string>()
                            };
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                res = null;
            }
            finally
            {
                await session.CloseAsync();
            }

            return res;
        }

        public async Task<List<Notification>> GetNotificationsByUser(string username)
        {
            IAsyncSession session = driver.AsyncSession();
            List<Notification> res = new List<Notification>();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        GetNotificationsForUserQuery(),
                        new Dictionary<string, object> {
                            { "Target", username }
                        });

                    while (await cursor.FetchAsync())
                    {
                        res.Add(new Notification
                        {
                            Id = cursor.Current["Id"].As<int>(),
                            Type = cursor.Current["Type"].As<string>(),
                            Author = cursor.Current["Author"].As<string>(),
                            Target = cursor.Current["Target"].As<string>(),
                            PostId = cursor.Current["PostId"].As<string>(),
                            CommentId = cursor.Current["CommentId"].As<string>(),
                            AnswerId = cursor.Current["AnswerId"].As<string>(),
                            SubmitDate = cursor.Current["SubmitDate"].As<DateTime>(),
                            AlreadySeen = cursor.Current["AlreadySeen"].As<bool>()
                        });
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                res = new List<Notification>();
            }
            finally
            {
                await session.CloseAsync();
            }

            return res;
        }

        public async Task<Notification> GetNotificationById(int notificationId)
        {
            IAsyncSession session = driver.AsyncSession();
            Notification res = null;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        GetNotificationByIdQuery(),
                        new Dictionary<string, object> {
                            { "Id", notificationId }
                        });

                    while (await cursor.FetchAsync())
                    {
                        res = new Notification
                        {
                            Id = cursor.Current["Id"].As<int>(),
                            Type = cursor.Current["Type"].As<string>(),
                            Author = cursor.Current["Author"].As<string>(),
                            Target = cursor.Current["Target"].As<string>(),
                            PostId = cursor.Current["PostId"].As<string>(),
                            CommentId = cursor.Current["CommentId"].As<string>(),
                            AnswerId = cursor.Current["AnswerId"].As<string>(),
                            SubmitDate = cursor.Current["SubmitDate"].As<DateTime>(),
                            AlreadySeen = cursor.Current["AlreadySeen"].As<bool>()
                        };
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                res = null;
            }
            finally
            {
                await session.CloseAsync();
            }

            return res;
        }

        public async Task<List<Notification>> UpdateNotificationsByUser(string username)
        {
            IAsyncSession session = driver.AsyncSession();
            List<Notification> res = new List<Notification>();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        UpdateNotificationsQuery(),
                        new Dictionary<string, object> {
                            { "Target", username },
                            { "Seen", true }
                        });

                    while (await cursor.FetchAsync())
                    {
                        res.Add(new Notification
                        {
                            Id = cursor.Current["Id"].As<int>(),
                            Type = cursor.Current["Type"].As<string>(),
                            Author = cursor.Current["Author"].As<string>(),
                            Target = cursor.Current["Target"].As<string>(),
                            PostId = cursor.Current["PostId"].As<string>(),
                            CommentId = cursor.Current["CommentId"].As<string>(),
                            AnswerId = cursor.Current["AnswerId"].As<string>(),
                            SubmitDate = cursor.Current["SubmitDate"].As<DateTime>(),
                            AlreadySeen = cursor.Current["AlreadySeen"].As<bool>()
                        });
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                res = new List<Notification>();
            }
            finally
            {
                await session.CloseAsync();
            }

            return res;
        }

        public async Task<Notification> UpdateNotificationById(int notificationId)
        {
            IAsyncSession session = driver.AsyncSession();
            Notification updatedNotification = null;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        UpdateNotificationByIdQuery(),
                        new Dictionary<string, object> {
                            { "NotificationId", notificationId },
                            { "Seen", true }
                        });

                    while (await cursor.FetchAsync())
                    {
                        updatedNotification = new Notification
                        {
                            Id = cursor.Current["Id"].As<int>(),
                            Type = cursor.Current["Type"].As<string>(),
                            Author = cursor.Current["Author"].As<string>(),
                            Target = cursor.Current["Target"].As<string>(),
                            PostId = cursor.Current["PostId"].As<string>(),
                            CommentId = cursor.Current["CommentId"].As<string>(),
                            AnswerId = cursor.Current["AnswerId"].As<string>(),
                            SubmitDate = cursor.Current["SubmitDate"].As<DateTime>(),
                            AlreadySeen = cursor.Current["AlreadySeen"].As<bool>()
                        };
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                updatedNotification = null;
            }
            finally
            {
                await session.CloseAsync();
            }

            return updatedNotification;
        }

        public async Task<SuccessResponse> DeleteNotificationsByUser(string username)
        {
            IAsyncSession session = driver.AsyncSession();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        DeleteAllNotificationsOfUserQuery(),
                        new Dictionary<string, object> {
                            { "Target", username }
                        });
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
            finally
            {
                await session.CloseAsync();
            }

            return new SuccessResponse();
        }

        public async Task<SuccessResponse> DeleteNotificationById(int notificationId)
        {
            IAsyncSession session = driver.AsyncSession();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        DeleteNotificationByIdQuery(),
                        new Dictionary<string, object> {
                            { "Id", notificationId }
                        });
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
            finally
            {
                await session.CloseAsync();
            }

            return new SuccessResponse();
        }
    }
}
