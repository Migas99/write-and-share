using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WriteAndShareWebApi.Interfaces.Repository;
using WriteAndShareWebApi.Models;
using static WriteAndShareWebApi.Neo4j.CypherQueries.ReactionQueries;

namespace WriteAndShareWebApi.Repository
{
    public class ReactionRepository : IReactionRepository
    {
        private readonly IDriver driver;
     
        public ReactionRepository(IDriver _driver)
        {
            driver = _driver;
        }

        public async Task<List<Post>> GetPostsReactedByUsername(string username)
        {
            IAsyncSession session = driver.AsyncSession();
            List<Post> posts = new List<Post>();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        GetPostsReactedByUserQuery(),
                        new Dictionary<string, object>
                        {
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

        public async Task<List<Comment>> GetCommentsReactedByUsername(string username)
        {
            IAsyncSession session = driver.AsyncSession();
            List<Comment> comments = new List<Comment>();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        GetCommentsReactedByUserQuery(),
                        new Dictionary<string, object> {
                            { "Username", username }
                        });

                    while (await cursor.FetchAsync())
                    {
                        comments.Add(new Comment
                        {
                            Id = cursor.Current["Id"].As<int>(),
                            Target = cursor.Current["Target"].As<int>(),
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
                comments = new List<Comment>();
            }
            finally
            {
                await session.CloseAsync();
            }

            return comments;
        }

        public async Task<bool> DidYouVote(string username, int id)
        {
            IAsyncSession session = driver.AsyncSession();
            bool didYouVote = false;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        DidYouVoteQuery(),
                        new Dictionary<string, object> {
                            { "Username", username },
                            { "Id", id }
                        });

                    while (await cursor.FetchAsync())
                    {
                        didYouVote = cursor.Current["DidYouVote"].As<bool>();
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                await session.CloseAsync();
            }

            return didYouVote;
        }

        public async Task<bool> DidYouUpvote(string username, int id)
        {
            IAsyncSession session = driver.AsyncSession();
            bool didYouUpvote = false;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        DidYouUpvoteQuery(),
                        new Dictionary<string, object> {
                            { "Username", username },
                            { "Id", id }
                        });

                    while (await cursor.FetchAsync())
                    {
                        didYouUpvote = cursor.Current["DidYouUpvote"].As<bool>();
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                await session.CloseAsync();
            }

            return didYouUpvote;
        }

        public async Task<bool> DidYouDownvote(string username, int id)
        {
            IAsyncSession session = driver.AsyncSession();
            bool didYouDownvote = false;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        DidYouDownvoteQuery(),
                        new Dictionary<string, object> {
                            { "Username", username },
                            { "Id", id }
                        });

                    while (await cursor.FetchAsync())
                    {
                        didYouDownvote = cursor.Current["DidYouDownvote"].As<bool>();
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                await session.CloseAsync();
            }

            return didYouDownvote;
        }

        public async Task<bool> Upvote(string username, int id)
        {
            IAsyncSession session = driver.AsyncSession();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        UpvoteQuery(),
                        new Dictionary<string, object> {
                            { "Username", username },
                            { "Id", id }
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

        public async Task<bool> Downvote(string username, int id)
        {
            IAsyncSession session = driver.AsyncSession();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        DownvoteQuery(),
                        new Dictionary<string, object> {
                            { "Username", username },
                            { "Id", id }
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

        public async Task<bool> DeleteReaction(string username, int id)
        {
            IAsyncSession session = driver.AsyncSession();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        RemoveVoteQuery(),
                        new Dictionary<string, object> {
                            { "Username", username },
                            { "Id", id }
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
