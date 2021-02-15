using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WriteAndShareWebApi.Interfaces.Repository;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.ResponseModels;
using static WriteAndShareWebApi.Neo4j.CypherQueries.CommentQueries;
using static WriteAndShareWebApi.Enums.DeletedValues;

namespace WriteAndShareWebApi.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IDriver driver;
        public CommentRepository(IDriver _driver)
        {
            driver = _driver;
        }

        public async Task<Comment> AddComment(Comment comment)
        {
            IAsyncSession session = driver.AsyncSession();
            Comment createdComment = null;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        CreateCommentQuery(),
                        new Dictionary<string, object> {
                            { "Target", comment.Target },
                            { "Message", comment.Message },
                            { "Author", comment.Author },
                            { "SubmitDate", DateTime.Now }
                        });

                    while (await cursor.FetchAsync())
                    {
                        createdComment = new Comment
                        {
                            Id = cursor.Current["Id"].As<int>(),
                            Target = cursor.Current["Target"].As<int>(),
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

                    if (comment.Mentions.Count > 0)
                    {
                        cursor = await tx.RunAsync(
                            CreateCommentMentionsQuery(),
                            new Dictionary<string, object> {
                                { "CommentId", createdComment.Id },
                                { "Mentions", comment.Mentions }
                            });

                        while (await cursor.FetchAsync())
                        {
                            createdComment.Mentions = cursor.Current["Mentions"].As<List<string>>();
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                createdComment = null;
            }
            finally
            {
                await session.CloseAsync();
            }

            return createdComment;
        }

        public async Task<List<Comment>> GetCommentsByUsername(string username)
        {
            IAsyncSession session = driver.AsyncSession();
            List<Comment> comments = new List<Comment>();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        GetCommentsByUserQuery(),
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

        public async Task<List<Comment>> GetCommentsByPublicationId(int postId)
        {
            IAsyncSession session = driver.AsyncSession();
            List<Comment> comments = new List<Comment>();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        GetCommentsByPostQuery(),
                        new Dictionary<string, object> {
                            { "PostId", postId }
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

        public async Task<List<Comment>> GetCommentsByCommentId(int commentId)
        {
            IAsyncSession session = driver.AsyncSession();
            List<Comment> comments = new List<Comment>();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        GetCommentsByCommentQuery(),
                        new Dictionary<string, object> {
                            { "CommentId", commentId }
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

        public async Task<Comment> GetCommentById(int commentId)
        {
            IAsyncSession session = driver.AsyncSession();
            Comment comment = null;

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        GetCommentByIdQuery(),
                        new Dictionary<string, object> {
                            { "CommentId", commentId }
                        });

                    while (await cursor.FetchAsync())
                    {
                        comment = new Comment
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
                        };
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                comment = null;
            }
            finally
            {
                await session.CloseAsync();
            }

            return comment;
        }

        public async Task<SuccessResponse> DeleteCommentById(int commentId)
        {
            IAsyncSession session = driver.AsyncSession();

            try
            {
                await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor cursor = await tx.RunAsync(
                        DeleteCommentByIdQuery(),
                        new Dictionary<string, object> {
                            { "CommentId", commentId }
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

            return new SuccessResponse { Success = "The comment was deleted with success." };
        }
    }
}
