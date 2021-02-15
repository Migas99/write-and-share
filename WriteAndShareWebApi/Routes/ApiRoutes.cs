namespace WriteAndShareWebApi.Controllers
{
    public class ApiRoutes
    {
        public static class Authentication
        {
            public const string Controller = "auth";

            public const string Register = "register";
            public const string Authenticate = "login";
        }

        public static class User
        {
            public const string Controller = "users";

            public const string GetAllUsersBasicInfo = "";
            public const string GetListOfUsersBasicInfoByUsernames = "";
            public const string GetUserByUsername = "{username}";

            public const string GetProfile = "profile";
            public const string UpdateHeader = "header";
            public const string UpdateAvatar = "avatar";
            public const string UpdateProfile = "profile";
            public const string DeleteProfile = "profile";
        }

        public static class Follower
        {
            public const string Controller = "";

            public const string GetFollowers = "followers";
            public const string GetFollowing = "following";

            public const string GetRequests = "requests";
            public const string GetRequested = "requested";

            public const string GetUserFollowers = "followers/{username}";
            public const string GetUserFollowing = "following/{username}";

            public const string Follow = "follow/{username}";
            public const string Unfollow = "unfollow/{username}";
            public const string RemoveFollower = "unfollower/{username}";
           
            public const string CancelRequest = "{requestId}/cancel";
            public const string AcceptRequest = "{requestId}/accept";
            public const string RefuseRequest = "{requestId}/refuse";
        }

        public static class Post
        {
            public const string Controller = "posts";

            public const string Feed = "feed";

            public const string GetAllPosts = "";
            public const string GetAllPostsByUsername = "{username}";
            public const string GetMyPosts = "myposts";
            public const string GetPostsMentioned = "postsmentioned";

            public const string GetPostById = "post/{postId}";
            public const string CreatePost = "new";
            public const string DeletePost = "post/{postId}";
        }

        public static class Comment
        {
            public const string Controller = "";

            public const string GetCommentsByUser = "users/{username}/comments";
            public const string GetCommentsByPublication = "posts/{postId}/comments";
            public const string GetCommentsByComment = "comments/{commentId}/comments";
            public const string GetMyComments = "comments/mycomments";

            public const string GetCommentById = "comments/{commentId}";
            public const string CreateComment = "comments/new";
            public const string DeleteComment = "comments/{commentId}";
        }

        public static class Reactions
        {
            public const string Controller = "";

            public const string GetReactionsOfUserToPosts = "reaction/{username}/posts";
            public const string GetReactionsOfUserToComments = "reaction/{username}/comments";
            public const string GetMyReactionsToPosts = "myreactions/posts";
            public const string GetMyReactionsToComments = "myreactions/comments";

            public const string Upvote = "reaction/{id}/upvote";
            public const string Downvote = "reaction/{id}/downvote";
            public const string DeleteVote = "reaction/{id}";
        }

        public static class Notifications
        {
            public const string Controller = "";

            public const string GetMyNotifications = "notifications";
            public const string UpdateMyNotifications = "notifications";
            public const string UpdateMyNotification = "notifications/{notificationId}";
            public const string DeleteMyNotifications = "notifications";
            public const string DeleteNotificationById = "notifications/{notificationId}";
        }
    }
}
