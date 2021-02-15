using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Interfaces.Repository;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static WriteAndShareWebApi.Neo4j.CypherQueries.FollowerQueries;

namespace WriteAndShareWebApi.Repository
{
    public class FollowerRepository : IFollowerRepository
    {
        private readonly IDriver driver;
        public FollowerRepository(IDriver _driver)
        {
            driver = _driver;
        }

        public async Task<bool> IsHeYourFollower(string user, string follower)
        {
            IAsyncSession session = driver.AsyncSession();
            bool isHeYourFollower = false;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        IsUserYourFollowerQuery(), 
                        new Dictionary<string, object> { 
                            { "User", user },
                            { "Follower", follower }
                        });

                    while (await cursor.FetchAsync())
                    {
                        isHeYourFollower = cursor.Current["IsHeYourFollower"].As<bool>();
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                isHeYourFollower = false;
            }
            finally
            {
                await session.CloseAsync();
            }

            return isHeYourFollower;
        }

        public async Task<bool> AreYouFollowingHim(string user, string following)
        {
            IAsyncSession session = driver.AsyncSession();
            bool areYouFollowingHim = false;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        AreYouFollowingUserQuery(),
                        new Dictionary<string, object> {
                            { "User", user },
                            { "Target", following }
                        });

                    while (await cursor.FetchAsync())
                    {
                        areYouFollowingHim = cursor.Current["AreYouFollowingHim"].As<bool>();
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                areYouFollowingHim = false;
            }
            finally
            {
                await session.CloseAsync();
            }

            return areYouFollowingHim;
        }

        public async Task<bool> DidYouRequestToFollow(string username, string target)
        {
            IAsyncSession session = driver.AsyncSession();
            bool didYouRequestToFollow = false;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        DidYouRequestToFollowQuery(),
                        new Dictionary<string, object> {
                            { "Username", username },
                            { "Target", target }
                        });

                    while (await cursor.FetchAsync())
                    {
                        didYouRequestToFollow = cursor.Current["DidYouRequestToFollow"].As<bool>();
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                didYouRequestToFollow = false;
            }
            finally
            {
                await session.CloseAsync();
            }

            return didYouRequestToFollow;
        }

        public async Task<List<User>> GetUserFollowers(string username)
        {
            IAsyncSession session = driver.AsyncSession();
            List<User> users = new List<User>();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(GetUserFollowersQuery(), new Dictionary<string, object> { { "Username", username } });
                    while (await cursor.FetchAsync())
                    {
                        users.Add(
                            new User
                            {
                                HeaderPath = cursor.Current["HeaderPath"].As<string>(),
                                AvatarPath = cursor.Current["AvatarPath"].As<string>(),
                                Username = cursor.Current["Username"].As<string>(),
                                Email = cursor.Current["Email"].As<string>(),
                                FirstName = cursor.Current["FirstName"].As<string>(),
                                LastName = cursor.Current["LastName"].As<string>(),
                                Gender = cursor.Current["Gender"].As<string>(),
                                BirthDate = cursor.Current["BirthDate"].As<string>(),
                                Telephone = cursor.Current["Telephone"].As<string>(),
                                Address = cursor.Current["Address"].As<string>(),
                                Privacy = cursor.Current["Privacy"].As<string>(),
                                Role = cursor.Current["Role"].As<string>()
                            }
                            );
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                users = new List<User>();
            }
            finally
            {
                await session.CloseAsync();
            }

            return users;
        }
        
        public async Task<List<User>> GetUsersFollowing(string username)
        {
            IAsyncSession session = driver.AsyncSession();
            List<User> users = new List<User>();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(GetUsersFollowedByUserQuery(), new Dictionary<string, object> { { "Username", username } });
                    while (await cursor.FetchAsync())
                    {
                        users.Add(
                            new User
                            {
                                HeaderPath = cursor.Current["HeaderPath"].As<string>(),
                                AvatarPath = cursor.Current["AvatarPath"].As<string>(),
                                Username = cursor.Current["Username"].As<string>(),
                                Email = cursor.Current["Email"].As<string>(),
                                FirstName = cursor.Current["FirstName"].As<string>(),
                                LastName = cursor.Current["LastName"].As<string>(),
                                Gender = cursor.Current["Gender"].As<string>(),
                                BirthDate = cursor.Current["BirthDate"].As<string>(),
                                Telephone = cursor.Current["Telephone"].As<string>(),
                                Address = cursor.Current["Address"].As<string>(),
                                Privacy = cursor.Current["Privacy"].As<string>(),
                                Role = cursor.Current["Role"].As<string>()
                            }
                            );
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                users = new List<User>();
            }
            finally
            {
                await session.CloseAsync();
            }

            return users;
        }

        public async Task<Request> GetRequestToUser(string username, int requestId)
        {
            IAsyncSession session = driver.AsyncSession();
            Request res = null;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        GetRequestMadeToUserQuery(),
                        new Dictionary<string, object> {
                            { "Username", username },
                            { "Request", requestId }
                        });

                    while (await cursor.FetchAsync())
                    {
                        res = new Request
                        {
                            Id = cursor.Current["Id"].As<int>(),
                            Requester = cursor.Current["Requester"].As<string>(),
                            Target = cursor.Current["Target"].As<string>(),
                            SubmitDate = cursor.Current["SubmitDate"].As<DateTime>()
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
        public async Task<List<Request>> GetRequestsToUser(string username)
        {
            IAsyncSession session = driver.AsyncSession();
            List<Request> requests = new List<Request>();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        GetAllRequestsMadeToUserQuery(), 
                        new Dictionary<string, object> {
                            { "Username", username } 
                        });

                    while (await cursor.FetchAsync())
                    {
                        requests.Add(
                            new Request
                            {
                                Id = cursor.Current["Id"].As<int>(),
                                Requester = cursor.Current["Requester"].As<string>(),
                                Target = cursor.Current["Target"].As<string>(),
                                SubmitDate = cursor.Current["SubmitDate"].As<DateTime>()
                            }
                            );
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                requests = new List<Request>();
            }
            finally
            {
                await session.CloseAsync();
            }

            return requests;
        }
        public async Task<Request> GetRequestMadeByUser(string username, int requestId)
        {
            IAsyncSession session = driver.AsyncSession();
            Request res = null;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        GetRequestMadeByUserQuery(),
                        new Dictionary<string, object> {
                            { "Username", username },
                            { "Request", requestId }
                        });

                    while (await cursor.FetchAsync())
                    {
                        res = new Request
                        {
                            Id = cursor.Current["Id"].As<int>(),
                            Requester = cursor.Current["Requester"].As<string>(),
                            Target = cursor.Current["Target"].As<string>(),
                            SubmitDate = cursor.Current["SubmitDate"].As<DateTime>()
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
        public async Task<List<Request>> GetRequestsMadeByUser(string username)
        {
            IAsyncSession session = driver.AsyncSession();
            List<Request> requests = new List<Request>();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        GetAllRequestsMadeByUserQuery(),
                        new Dictionary<string, object> {
                            { "Username", username }
                        });

                    while (await cursor.FetchAsync())
                    {
                        requests.Add(
                            new Request
                            {
                                Id = cursor.Current["Id"].As<int>(),
                                Requester = cursor.Current["Requester"].As<string>(),
                                Target = cursor.Current["Target"].As<string>(),
                                SubmitDate = cursor.Current["SubmitDate"].As<DateTime>()
                            }
                            );
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                requests = new List<Request>();
            }
            finally
            {
                await session.CloseAsync();
            }

            return requests;
        }
        public async Task<bool> AddAsFollower(string requester, string target)
        {
            IAsyncSession session = driver.AsyncSession();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    await tx.RunAsync(
                        FollowUserQuery(),
                        new Dictionary<string, object> {
                            { "Requester", requester },
                            { "Target", target },
                            { "SubmitDate", DateTime.Now }
                        });
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                await session.CloseAsync();
            }

            return true;
        }
        public async Task<bool> AddRequestToFollow(string requester, string target)
        {
            IAsyncSession session = driver.AsyncSession();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    await tx.RunAsync(
                        CreateRequestToFollowQuery(),
                        new Dictionary<string, object> {
                            { "Requester", requester },
                            { "Target", target },
                            { "SubmitDate", DateTime.Now }
                        });
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                await session.CloseAsync();
            }

            return true;
        }
        public async Task<bool> RemoveAsFollower(string follower, string target)
        {
            IAsyncSession session = driver.AsyncSession();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    await tx.RunAsync(
                        UnfollowUserQuery(),
                        new Dictionary<string, object> {
                            { "Follower", follower },
                            { "Target", target }
                        });
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                await session.CloseAsync();
            }

            return true;
        }
        public async Task<bool> AcceptRequest(int requestId)
        {
            IAsyncSession session = driver.AsyncSession();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    await tx.RunAsync(
                        AcceptRequestQuery(),
                        new Dictionary<string, object> {
                            { "Request", requestId }
                        });
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                await session.CloseAsync();
            }

            return true;
        }

        public async Task<bool> RemoveRequest(int requestId)
        {
            IAsyncSession session = driver.AsyncSession();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    await tx.RunAsync(
                        DeleteRequestQuery(),
                        new Dictionary<string, object> {
                            { "Request", requestId }
                        });
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                await session.CloseAsync();
            }

            return true;
        }
    }
}
