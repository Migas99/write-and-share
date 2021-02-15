using WriteAndShareWebApi.Interfaces;
using WriteAndShareWebApi.Models;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static WriteAndShareWebApi.Neo4j.CypherQueries.UserQueries;
using static WriteAndShareWebApi.Enums.DeletedValues;
using WriteAndShareWebApi.Models.ResponseModels;
using WriteAndShareWebApi.Enums;

namespace WriteAndShareWebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDriver driver;
        public UserRepository(IDriver _driver)
        {
            driver = _driver;
        }

        public async Task<User> AddUser(User user)
        {
            IAsyncSession session = driver.AsyncSession();
            User createdUser = null;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                 {
                     IResultCursor cursor = await tx.RunAsync(
                         CreateUserQuery(),
                         new Dictionary<string, object> {
                             { "HeaderPath", user.HeaderPath },
                             { "AvatarPath", user.AvatarPath },
                             { "Username", user.Username },
                             { "HashedPassword", user.HashedPassword },
                             { "Email", user.Email },
                             { "FirstName", user.FirstName },
                             { "LastName", user.LastName },
                             { "Gender", user.Gender },
                             { "BirthDate", user.BirthDate },
                             { "Telephone", user.Telephone },
                             { "Address", user.Address },
                             { "Privacy", user.Privacy },
                             { "Role", user.Role },
                         });

                     while (await cursor.FetchAsync())
                     {
                         createdUser = new User
                         {
                             HeaderPath = cursor.Current["HeaderPath"].As<string>(),
                             AvatarPath = cursor.Current["AvatarPath"].As<string>(),
                             Username = cursor.Current["Username"].As<string>(),
                             HashedPassword = cursor.Current["HashedPassword"].As<string>(),
                             Email = cursor.Current["Email"].As<string>(),
                             FirstName = cursor.Current["FirstName"].As<string>(),
                             LastName = cursor.Current["LastName"].As<string>(),
                             Gender = cursor.Current["Gender"].As<string>(),
                             BirthDate = cursor.Current["BirthDate"].As<string>(),
                             Telephone = cursor.Current["Telephone"].As<string>(),
                             Address = cursor.Current["Address"].As<string>(),
                             Privacy = cursor.Current["Privacy"].As<string>(),
                             Role = cursor.Current["Role"].As<string>()
                         };
                     }
                 });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                createdUser = null;
            }
            finally
            {
                await session.CloseAsync();
            }

            return createdUser;
        }

        public async Task<User> UpdateUser(User user)
        {
            IAsyncSession session = driver.AsyncSession();
            User updatedUser = null;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        UpdateUserQuery(), 
                        new Dictionary<string, object> { 
                            { "Username", user.Username },
                            { "HashedPassword", user.HashedPassword },
                            { "Email", user.Email },
                            { "Telephone", user.Telephone },
                            { "Address", user.Address },
                            { "Privacy", user.Privacy }
                        });

                    while (await cursor.FetchAsync())
                    {
                        updatedUser = new User
                        {
                            HeaderPath = cursor.Current["HeaderPath"].As<string>(),
                            AvatarPath = cursor.Current["AvatarPath"].As<string>(),
                            Username = cursor.Current["Username"].As<string>(),
                            HashedPassword = cursor.Current["HashedPassword"].As<string>(),
                            Email = cursor.Current["Email"].As<string>(),
                            FirstName = cursor.Current["FirstName"].As<string>(),
                            LastName = cursor.Current["LastName"].As<string>(),
                            Gender = cursor.Current["Gender"].As<string>(),
                            BirthDate = cursor.Current["BirthDate"].As<string>(),
                            Telephone = cursor.Current["Telephone"].As<string>(),
                            Address = cursor.Current["Address"].As<string>(),
                            Privacy = cursor.Current["Privacy"].As<string>(),
                            Role = cursor.Current["Role"].As<string>()
                        };
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                updatedUser = null;
            }
            finally
            {
                await session.CloseAsync();
            }

            return updatedUser;
        }

        public async Task<SuccessResponse> DeleteUserByUsername(string username)
        {
            IAsyncSession session = driver.AsyncSession();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        DeleteUserByUsernameQuery(),
                        new Dictionary<string, object> {
                            { "Username", username },
                            { "HeaderPath", Uploads.DefaultHeader },
                            { "AvatarPath", Uploads.DefaultAvatar },
                            { "Email", Removed },
                            { "FirstName", Removed },
                            { "LastName", Removed },
                            { "Gender", Removed },
                            { "BirthDate", DateTime.Now },
                            { "Telephone", Removed },
                            { "Address", Removed },
                            { "Privacy", Privacies.Desactivated },
                            { "Role", Roles.Desactivated }
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

            return new SuccessResponse { Success = "The user was deleted with success." };
        }

        public async Task<User> UpdateUserHeader(string username, string headerPath)
        {
            IAsyncSession session = driver.AsyncSession();
            User updatedUser = null;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        UpdateUserHeaderQuery(),
                        new Dictionary<string, object> {
                            { "Username", username },
                            { "HeaderPath", headerPath }
                        });

                    while (await cursor.FetchAsync())
                    {
                        updatedUser = new User
                        {
                            HeaderPath = cursor.Current["HeaderPath"].As<string>(),
                            AvatarPath = cursor.Current["AvatarPath"].As<string>(),
                            Username = cursor.Current["Username"].As<string>(),
                            HashedPassword = cursor.Current["HashedPassword"].As<string>(),
                            Email = cursor.Current["Email"].As<string>(),
                            FirstName = cursor.Current["FirstName"].As<string>(),
                            LastName = cursor.Current["LastName"].As<string>(),
                            Gender = cursor.Current["Gender"].As<string>(),
                            BirthDate = cursor.Current["BirthDate"].As<string>(),
                            Telephone = cursor.Current["Telephone"].As<string>(),
                            Address = cursor.Current["Address"].As<string>(),
                            Privacy = cursor.Current["Privacy"].As<string>(),
                            Role = cursor.Current["Role"].As<string>()
                        };
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                updatedUser = null;
            }
            finally
            {
                await session.CloseAsync();
            }

            return updatedUser;
        }

        public async Task<User> UpdateUserAvatar(string username, string avatarPath)
        {
            IAsyncSession session = driver.AsyncSession();
            User updatedUser = null;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        UpdateUserAvatarQuery(),
                        new Dictionary<string, object> {
                            { "Username", username },
                            { "AvatarPath", avatarPath }
                        });

                    while (await cursor.FetchAsync())
                    {
                        updatedUser = new User
                        {
                            HeaderPath = cursor.Current["HeaderPath"].As<string>(),
                            AvatarPath = cursor.Current["AvatarPath"].As<string>(),
                            Username = cursor.Current["Username"].As<string>(),
                            HashedPassword = cursor.Current["HashedPassword"].As<string>(),
                            Email = cursor.Current["Email"].As<string>(),
                            FirstName = cursor.Current["FirstName"].As<string>(),
                            LastName = cursor.Current["LastName"].As<string>(),
                            Gender = cursor.Current["Gender"].As<string>(),
                            BirthDate = cursor.Current["BirthDate"].As<string>(),
                            Telephone = cursor.Current["Telephone"].As<string>(),
                            Address = cursor.Current["Address"].As<string>(),
                            Privacy = cursor.Current["Privacy"].As<string>(),
                            Role = cursor.Current["Role"].As<string>()
                        };
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                updatedUser = null;
            }
            finally
            {
                await session.CloseAsync();
            }

            return updatedUser;
        }

        public async Task<List<User>> GetAllUsers()
        {
            IAsyncSession session = driver.AsyncSession();
            List<User> users = new List<User>();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(GetAllUsersQuery());
                    while (await cursor.FetchAsync())
                    {
                        users.Add(new User
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
                        });
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

        public async Task<List<User>> GetListOfUsersByUsernames(List<string> usernames)
        {
            IAsyncSession session = driver.AsyncSession();
            List<User> users = new List<User>();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        GetListOfUsers(),
                        new Dictionary<string, object>
                        {
                            { "Usernames", usernames }
                        });
                    while (await cursor.FetchAsync())
                    {
                        users.Add(new User
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
                        });
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

        public async Task<User> GetUserByUsername(string username)
        {
            IAsyncSession session = driver.AsyncSession();
            User user = null;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(GetUserByUsernameQuery(), new Dictionary<string, object> { { "Username", username } });
                    while(await cursor.FetchAsync())
                    {
                        user = new User
                        {
                            HeaderPath = cursor.Current["HeaderPath"].As<string>(),
                            AvatarPath = cursor.Current["AvatarPath"].As<string>(),
                            Username = cursor.Current["Username"].As<string>(),
                            HashedPassword = cursor.Current["HashedPassword"].As<string>(),
                            Email = cursor.Current["Email"].As<string>(),
                            FirstName = cursor.Current["FirstName"].As<string>(),
                            LastName = cursor.Current["LastName"].As<string>(),
                            Gender = cursor.Current["Gender"].As<string>(),
                            BirthDate = cursor.Current["BirthDate"].As<string>(),
                            Telephone = cursor.Current["Telephone"].As<string>(),
                            Address = cursor.Current["Address"].As<string>(),
                            Privacy = cursor.Current["Privacy"].As<string>(),
                            Role = cursor.Current["Role"].As<string>()
                        };
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                user = null;
            }
            finally
            {
                await session.CloseAsync();
            }

            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            IAsyncSession session = driver.AsyncSession();
            User user = null;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(GetUserByEmailQuery(), new Dictionary<string, object> { { "Email", email } });
                    while (await cursor.FetchAsync())
                    {
                        user = new User
                        {
                            HeaderPath = cursor.Current["HeaderPath"].As<string>(),
                            AvatarPath = cursor.Current["AvatarPath"].As<string>(),
                            Username = cursor.Current["Username"].As<string>(),
                            HashedPassword = cursor.Current["HashedPassword"].As<string>(),
                            Email = cursor.Current["Email"].As<string>(),
                            FirstName = cursor.Current["FirstName"].As<string>(),
                            LastName = cursor.Current["LastName"].As<string>(),
                            Gender = cursor.Current["Gender"].As<string>(),
                            BirthDate = cursor.Current["BirthDate"].As<string>(),
                            Telephone = cursor.Current["Telephone"].As<string>(),
                            Address = cursor.Current["Address"].As<string>(),
                            Privacy = cursor.Current["Privacy"].As<string>(),
                            Role = cursor.Current["Role"].As<string>()
                        };
                    }
                });
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                user = null;
            }
            finally
            {
                await session.CloseAsync();
            }

            return user;
        }
    }
}
