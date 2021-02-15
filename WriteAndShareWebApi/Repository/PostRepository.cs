using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WriteAndShareWebApi.Interfaces.Repository;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.ResponseModels;
using static WriteAndShareWebApi.Neo4j.CypherQueries.PostQueries;
using static WriteAndShareWebApi.Enums.DeletedValues;

namespace WriteAndShareWebApi.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly IDriver driver;
        public PostRepository(IDriver _driver)
        {
            driver = _driver;
        }

        public async Task<Post> AddPost(Post post)
        {
            IAsyncSession session = driver.AsyncSession();
            Post createdPost = null;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        CreatePostQuery(),
                        new Dictionary<string, object> {
                            { "UploadPath", post.UploadPath },
                            { "Link", post.Link },
                            { "Message", post.Message },
                            { "Author", post.Author },
                            { "SubmitDate", DateTime.Now }
                        });

                    while (await cursor.FetchAsync())
                    {
                        createdPost = new Post
                        {
                            Id = cursor.Current["Id"].As<int>(),
                            UploadPath = cursor.Current["UploadPath"].As<string>(),
                            Link = cursor.Current["Link"].As<string>(),
                            Message = cursor.Current["Message"].As<string>(),
                            Author = cursor.Current["Author"].As<string>(),
                            SubmitDate = cursor.Current["SubmitDate"].As<DateTime>(),
                            Upvoted = new List<string>(),
                            Downvoted = new List<string>(),
                            Score = 0,
                            Mentions = new List<string>(),
                            CommentsNumber = 0
                        };
                    }

                    if(post.Mentions.Count > 0)
                    {
                        cursor = await tx.RunAsync(
                            CreatePostMentionsQuery(),
                            new Dictionary<string, object> {
                                { "PostId", createdPost.Id },
                                { "Mentions", post.Mentions }
                            });

                        while(await cursor.FetchAsync())
                        {
                            createdPost.Mentions = cursor.Current["Mentions"].As<List<string>>();
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                createdPost = null;
            }
            finally
            {
                await session.CloseAsync();
            }

            return createdPost;
        }

        public async Task<List<Post>> GetFeed(string username)
        {
            IAsyncSession session = driver.AsyncSession();
            List<Post> posts = new List<Post>();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        GetFeedQuery(),
                        new Dictionary<string, object>
                        {
                            { "Requester", username }
                        });
                    while (await cursor.FetchAsync())
                    {
                        posts.Add(new Post
                        {
                            Id = cursor.Current["Id"].As<int>(),
                            UploadPath = cursor.Current["UploadPath"].As<string>(),
                            Link = cursor.Current["Link"].As<string>(),
                            Message = cursor.Current["Message"].As<string>(),
                            Author = cursor.Current["Author"].As<string>(),
                            SubmitDate = cursor.Current["SubmitDate"].As<DateTime>(),
                            Upvoted = cursor.Current["Upvoted"].As<List<string>>(),
                            Downvoted = cursor.Current["Downvoted"].As<List<string>>(),
                            Score = cursor.Current["Score"].As<int>(),
                            Mentions = cursor.Current["Mentions"].As<List<string>>(),
                            CommentsNumber = cursor.Current["CommentsNumber"].As<int>()
                        });
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                posts = new List<Post>();
            }
            finally
            {
                await session.CloseAsync();
            }

            return posts;
        }

        public async Task<List<Post>> GetAllPosts()
        {
            IAsyncSession session = driver.AsyncSession();
            List<Post> posts = new List<Post>();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(GetAllPostsQuery());
                    while (await cursor.FetchAsync())
                    {
                        posts.Add(new Post
                        {
                            Id = cursor.Current["Id"].As<int>(),
                            UploadPath = cursor.Current["UploadPath"].As<string>(),
                            Link = cursor.Current["Link"].As<string>(),
                            Message = cursor.Current["Message"].As<string>(),
                            Author = cursor.Current["Author"].As<string>(),
                            SubmitDate = cursor.Current["SubmitDate"].As<DateTime>(),
                            Upvoted = cursor.Current["Upvoted"].As<List<string>>(),
                            Downvoted = cursor.Current["Downvoted"].As<List<string>>(),
                            Score = cursor.Current["Score"].As<int>(),
                            Mentions = cursor.Current["Mentions"].As<List<string>>(),
                            CommentsNumber = cursor.Current["CommentsNumber"].As<int>()
                        });
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                posts = new List<Post>();
            }
            finally
            {
                await session.CloseAsync();
            }

            return posts;
        }

        public async Task<List<Post>> GetAllWatchablePostsForUser(string username)
        {
            IAsyncSession session = driver.AsyncSession();
            List<Post> posts = new List<Post>();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        GetAllWatchablePostsForUserQuery(),
                        new Dictionary<string, object> {
                            { "Requester", username }
                        });

                    while (await cursor.FetchAsync())
                    {
                        posts.Add(new Post
                        {
                            Id = cursor.Current["Id"].As<int>(),
                            UploadPath = cursor.Current["UploadPath"].As<string>(),
                            Link = cursor.Current["Link"].As<string>(),
                            Message = cursor.Current["Message"].As<string>(),
                            Author = cursor.Current["Author"].As<string>(),
                            SubmitDate = cursor.Current["SubmitDate"].As<DateTime>(),
                            Upvoted = cursor.Current["Upvoted"].As<List<string>>(),
                            Downvoted = cursor.Current["Downvoted"].As<List<string>>(),
                            Score = cursor.Current["Score"].As<int>(),
                            Mentions = cursor.Current["Mentions"].As<List<string>>(),
                            CommentsNumber = cursor.Current["CommentsNumber"].As<int>()
                        });
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                posts = new List<Post>();
            }
            finally
            {
                await session.CloseAsync();
            }

            return posts;
        }

        public async Task<List<Post>> GetPostsMadeByUser(string username)
        {
            IAsyncSession session = driver.AsyncSession();
            List<Post> posts = new List<Post>();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        GetPostsMadeByUserQuery(),
                        new Dictionary<string, object> {
                            { "Author", username }
                        });

                    while (await cursor.FetchAsync())
                    {
                        posts.Add(new Post
                        {
                            Id = cursor.Current["Id"].As<int>(),
                            UploadPath = cursor.Current["UploadPath"].As<string>(),
                            Link = cursor.Current["Link"].As<string>(),
                            Message = cursor.Current["Message"].As<string>(),
                            Author = cursor.Current["Author"].As<string>(),
                            SubmitDate = cursor.Current["SubmitDate"].As<DateTime>(),
                            Upvoted = cursor.Current["Upvoted"].As<List<string>>(),
                            Downvoted = cursor.Current["Downvoted"].As<List<string>>(),
                            Score = cursor.Current["Score"].As<int>(),
                            Mentions = cursor.Current["Mentions"].As<List<string>>(),
                            CommentsNumber = cursor.Current["CommentsNumber"].As<int>()
                        });
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                posts = new List<Post>();
            }
            finally
            {
                await session.CloseAsync();
            }

            return posts;
        }

        public async Task<List<Post>> GetPostsUserWasMentioned(string username)
        {
            IAsyncSession session = driver.AsyncSession();
            List<Post> posts = new List<Post>();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        GetPostsMentionedUserQuery(),
                        new Dictionary<string, object> {
                            { "Username", username }
                        });

                    while (await cursor.FetchAsync())
                    {
                        posts.Add(new Post
                        {
                            Id = cursor.Current["Id"].As<int>(),
                            UploadPath = cursor.Current["UploadPath"].As<string>(),
                            Link = cursor.Current["Link"].As<string>(),
                            Message = cursor.Current["Message"].As<string>(),
                            Author = cursor.Current["Author"].As<string>(),
                            SubmitDate = cursor.Current["SubmitDate"].As<DateTime>(),
                            Upvoted = cursor.Current["Upvoted"].As<List<string>>(),
                            Downvoted = cursor.Current["Downvoted"].As<List<string>>(),
                            Score = cursor.Current["Score"].As<int>(),
                            Mentions = cursor.Current["Mentions"].As<List<string>>(),
                            CommentsNumber = cursor.Current["CommentsNumber"].As<int>()
                        });
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                posts = new List<Post>();
            }
            finally
            {
                await session.CloseAsync();
            }

            return posts;
        }

        public async Task<Post> GetPostById(int postId)
        {
            IAsyncSession session = driver.AsyncSession();
            Post post = null;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        GetPostByIdQuery(),
                        new Dictionary<string, object> {
                            { "PostId", postId }
                        });

                    while (await cursor.FetchAsync())
                    {
                        post = new Post
                        {
                            Id = cursor.Current["Id"].As<int>(),
                            UploadPath = cursor.Current["UploadPath"].As<string>(),
                            Link = cursor.Current["Link"].As<string>(),
                            Message = cursor.Current["Message"].As<string>(),
                            Author = cursor.Current["Author"].As<string>(),
                            SubmitDate = cursor.Current["SubmitDate"].As<DateTime>(),
                            Upvoted = cursor.Current["Upvoted"].As<List<string>>(),
                            Downvoted = cursor.Current["Downvoted"].As<List<string>>(),
                            Score = cursor.Current["Score"].As<int>(),
                            Mentions = cursor.Current["Mentions"].As<List<string>>(),
                            CommentsNumber = cursor.Current["CommentsNumber"].As<int>()
                        };
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                post = null;
            }
            finally
            {
                await session.CloseAsync();
            }

            return post;
        }

        public async Task<SuccessResponse> DeletePostById(int postId)
        {
            IAsyncSession session = driver.AsyncSession();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        DeletePostByIdQuery(),
                        new Dictionary<string, object> {
                            { "PostId", postId }
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

            return new SuccessResponse { Success = "The post was deleted with success." };
        }
    }
}
