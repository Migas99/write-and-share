namespace WriteAndShareWebApi.Neo4j
{
    public static class CypherQueries
    {
        //Labels
        private const string User = "User";
        private const string Post = "Post";
        private const string Comment = "Comment";
        private const string Notification = "Notification";

        //Relationships
        private const string Follows = "FOLLOWS";
        private const string Request = "REQUESTED_TO_FOLLOW";

        private const string Posted = "POSTED";
        private const string Upvoted = "UPVOTED";
        private const string Downvoted = "DOWNVOTED";

        private const string Mentioned = "MENTIONED";

        private const string Commented = "COMMENTED";
        private const string CommentTarget = "ON";

        private const string Notifies = "NOTIFIES";
        private const string Responsible = "RESPONSIBLE";
        private const string RelatedToPost = "HAS_POST";
        private const string RelatedToComment = "HAS_COMMENT";
        private const string RelatedToAnswer = "HAS_ANSWER";


        public static class UserQueries
        {
            public static string CreateUserQuery()
            {
                return "CREATE (user:" + User + " " +
                        "{" +
                        "HeaderPath: $HeaderPath," +
                        "AvatarPath: $AvatarPath," +
                        "Username: $Username," +
                        "HashedPassword: $HashedPassword," +
                        "Email: $Email," +
                        "FirstName: $FirstName," +
                        "LastName: $LastName," +
                        "Gender: $Gender," +
                        "BirthDate: $BirthDate," +
                        "Telephone: $Telephone," +
                        "Address: $Address," +
                        "Privacy: $Privacy," +
                        "Role: $Role" +
                        "})\n" +
                        "RETURN " +
                        "user.HeaderPath AS HeaderPath," +
                        "user.AvatarPath AS AvatarPath," +
                        "user.Username AS Username," +
                        "user.HashedPassword AS HashedPassword," +
                        "user.Email AS Email," +
                        "user.FirstName AS FirstName," +
                        "user.LastName AS LastName," +
                        "user.Gender AS Gender," +
                        "user.BirthDate AS BirthDate," +
                        "user.Telephone AS Telephone," +
                        "user.Address AS Address," +
                        "user.Privacy AS Privacy," +
                        "user.Role AS Role";
            }

            public static string UpdateUserQuery()
            {
                return "MATCH (user:" + User + ")\n" +
                       "WHERE user.Username = $Username\n" +
                       "SET user.HashedPassword = $HashedPassword\n" +
                       "SET user.Email = $Email\n" +
                       "SET user.Telephone = $Telephone\n" +
                       "SET user.Address = $Address\n" +
                       "SET user.Privacy = $Privacy\n" +
                       "RETURN " +
                       "user.HeaderPath AS HeaderPath," +
                       "user.AvatarPath AS AvatarPath," +
                       "user.Username AS Username," +
                       "user.HashedPassword AS HashedPassword," +
                       "user.Email AS Email," +
                       "user.FirstName AS FirstName," +
                       "user.LastName AS LastName," +
                       "user.Gender AS Gender," +
                       "user.BirthDate AS BirthDate," +
                       "user.Telephone AS Telephone," +
                       "user.Address AS Address," +
                       "user.Privacy AS Privacy," +
                       "user.Role AS Role";
            }

            public static string UpdateUserHeaderQuery()
            {
                return "MATCH (user:" + User + ")\n" +
                       "WHERE user.Username = $Username\n" +
                       "SET user.HeaderPath = $HeaderPath\n" +
                       "RETURN " +
                       "user.HeaderPath AS HeaderPath," +
                       "user.AvatarPath AS AvatarPath," +
                       "user.Username AS Username," +
                       "user.HashedPassword AS HashedPassword," +
                       "user.Email AS Email," +
                       "user.FirstName AS FirstName," +
                       "user.LastName AS LastName," +
                       "user.Gender AS Gender," +
                       "user.BirthDate AS BirthDate," +
                       "user.Telephone AS Telephone," +
                       "user.Address AS Address," +
                       "user.Privacy AS Privacy," +
                       "user.Role AS Role";

            }

            public static string UpdateUserAvatarQuery()
            {
                return "MATCH (user:" + User + ")\n" +
                       "WHERE user.Username = $Username\n" +
                       "SET user.AvatarPath = $AvatarPath\n" +
                       "RETURN " +
                       "user.HeaderPath AS HeaderPath," +
                       "user.AvatarPath AS AvatarPath," +
                       "user.Username AS Username," +
                       "user.HashedPassword AS HashedPassword," +
                       "user.Email AS Email," +
                       "user.FirstName AS FirstName," +
                       "user.LastName AS LastName," +
                       "user.Gender AS Gender," +
                       "user.BirthDate AS BirthDate," +
                       "user.Telephone AS Telephone," +
                       "user.Address AS Address," +
                       "user.Privacy AS Privacy," +
                       "user.Role AS Role";

            }

            public static string GetAllUsersQuery()
            {
                return "MATCH (user:" + User + ")\n" +
                       "RETURN " +
                       "user.HeaderPath AS HeaderPath," +
                       "user.AvatarPath AS AvatarPath," +
                       "user.Username AS Username," +
                       "user.HashedPassword AS HashedPassword," +
                       "user.Email AS Email," +
                       "user.FirstName AS FirstName," +
                       "user.LastName AS LastName," +
                       "user.Gender AS Gender," +
                       "user.BirthDate AS BirthDate," +
                       "user.Telephone AS Telephone," +
                       "user.Address AS Address," +
                       "user.Privacy AS Privacy," +
                       "user.Role AS Role";
            }

            public static string GetListOfUsers()
            {
                return "UNWIND $Usernames as value\n" +
                       "MATCH (user:" + User + ")\n" +
                       "WHERE user.Username = value\n" +
                       "RETURN " +
                       "user.HeaderPath AS HeaderPath," +
                       "user.AvatarPath AS AvatarPath," +
                       "user.Username AS Username," +
                       "user.HashedPassword AS HashedPassword," +
                       "user.Email AS Email," +
                       "user.FirstName AS FirstName," +
                       "user.LastName AS LastName," +
                       "user.Gender AS Gender," +
                       "user.BirthDate AS BirthDate," +
                       "user.Telephone AS Telephone," +
                       "user.Address AS Address," +
                       "user.Privacy AS Privacy," +
                       "user.Role AS Role";
            }

            public static string GetUserByUsernameQuery()
            {
                return "MATCH (user:" + User + ")\n" +
                       "WHERE user.Username = $Username\n" +
                       "RETURN " +
                       "user.HeaderPath AS HeaderPath," +
                       "user.AvatarPath AS AvatarPath," +
                       "user.Username AS Username," +
                       "user.HashedPassword AS HashedPassword," +
                       "user.Email AS Email," +
                       "user.FirstName AS FirstName," +
                       "user.LastName AS LastName," +
                       "user.Gender AS Gender," +
                       "user.BirthDate AS BirthDate," +
                       "user.Telephone AS Telephone," +
                       "user.Address AS Address," +
                       "user.Privacy AS Privacy," +
                       "user.Role AS Role";
            }

            public static string GetUserByEmailQuery()
            {
                return "MATCH (user:" + User + ")\n" +
                       "WHERE user.Email = $Email\n" +
                       "RETURN " +
                       "user.HeaderPath AS HeaderPath," +
                       "user.AvatarPath AS AvatarPath," +
                       "user.Username AS Username," +
                       "user.HashedPassword AS HashedPassword," +
                       "user.Email AS Email," +
                       "user.FirstName AS FirstName," +
                       "user.LastName AS LastName," +
                       "user.Gender AS Gender," +
                       "user.BirthDate AS BirthDate," +
                       "user.Telephone AS Telephone," +
                       "user.Address AS Address," +
                       "user.Privacy AS Privacy," +
                       "user.Role AS Role";
            }

            public static string DeleteUserByUsernameQuery()
            {
                return "MATCH (user:" + User + ")\n" +
                       "WHERE user.Username = $Username\n" +
                       "SET user.HeaderPath = $HeaderPath\n" +
                       "SET user.AvatarPath = $AvatarPath\n" +
                       "SET user.Email = $Email\n" +
                       "SET user.FirstName = $FirstName\n" +
                       "SET user.LastName = $LastName\n" +
                       "SET user.Gender = $Gender\n" +
                       "SET user.BirthDate = $BirthDate\n" +
                       "SET user.Telephone = $Telephone\n" +
                       "SET user.Address = $Address\n" +
                       "SET user.Privacy = $Privacy\n" +
                       "SET user.Role = $Role\n" +
                       "WITH user\n" +
                       "MATCH (user)-[r:" + Follows + "]->(other:" + User + ")\n" +
                       "DELETE r\n" +
                       "WITH user\n" +
                       "MATCH (other:" + User + ")-[r:" + Follows + "]->(user)\n" +
                       "DELETE r\n" +
                       "WITH user\n" +
                       "MATCH (user)-[:" + Posted + "]->(post:" + Post + ")\n" +
                       "DETACH DELETE post\n" +
                       "WITH user\n" +
                       "MATCH (user)-[:" + Commented + "]->(comment:" + Comment + ")\n" +
                       "DETACH DELETE comment";
            }
        }

        public static class FollowerQueries
        {
            public static string IsUserYourFollowerQuery()
            {
                return "MATCH (follower:" + User + ")-[r:" + Follows + "]->(target:" + User + ")\n" +
                       "WHERE follower.Username = $Follower AND target.Username = $User\n" +
                       "RETURN count(r) > 0 AS IsHeYourFollower";
            }

            public static string AreYouFollowingUserQuery()
            {
                return "MATCH (follower:" + User + ")-[r:" + Follows + "]->(target:" + User + ")\n" +
                       "WHERE follower.Username = $User AND target.Username = $Target\n" +
                       "RETURN count(r) > 0 AS AreYouFollowingHim";
            }

            public static string GetUserFollowersQuery()
            {
                return "MATCH (user1:" + User + ")-[:" + Follows + "]->(user2:" + User + ")\n" +
                       "WHERE user2.Username = $Username\n" +
                       "WITH user1, user2\n" +
                       "RETURN " +
                       "user1.HeaderPath AS HeaderPath," +
                       "user1.AvatarPath AS AvatarPath," +
                       "user1.Username AS Username," +
                       "user1.Email AS Email," +
                       "user1.FirstName AS FirstName," +
                       "user1.LastName AS LastName," +
                       "user1.Gender AS Gender," +
                       "user1.BirthDate AS BirthDate," +
                       "user1.Telephone AS Telephone," +
                       "user1.Address AS Address," +
                       "user1.Privacy AS Privacy," +
                       "user1.Role AS Role";
            }

            public static string GetUsersFollowedByUserQuery()
            {
                return "MATCH (follower:" + User + ")-[:" + Follows + "]->(target:" + User + ")\n" +
                       "WHERE follower.Username = $Username\n" +
                       "RETURN " +
                       "target.HeaderPath AS HeaderPath," +
                       "target.AvatarPath AS AvatarPath," +
                       "target.Username AS Username," +
                       "target.Email AS Email," +
                       "target.FirstName AS FirstName," +
                       "target.LastName AS LastName," +
                       "target.Gender AS Gender," +
                       "target.BirthDate AS BirthDate," +
                       "target.Telephone AS Telephone," +
                       "target.Address AS Address," +
                       "target.Privacy AS Privacy," +
                       "target.Role AS Role";
            }

            public static string DidYouRequestToFollowQuery()
            {
                return "MATCH (requester:" + User + ")-[r:" + Request + "]->(target:" + User + ")\n" +
                       "WHERE requester.Username = $Username AND target.Username = $Target\n" +
                       "RETURN count(r) > 0 AS DidYouRequestToFollow";
            }

            public static string GetRequestMadeToUserQuery()
            {
                return "MATCH (requester:" + User + ")-[r:" + Request + "]->(target:" + User + ")\n" +
                       "WHERE target.Username = $Username AND ID(r) = $Request\n" +
                       "RETURN " +
                       "ID(r) AS Id," +
                       "requester.Username AS Requester," +
                       "target.Username AS Target," +
                       "r.SubmitDate AS SubmitDate";
            }

            public static string GetAllRequestsMadeToUserQuery()
            {
                return "MATCH (requester:" + User + ")-[r:" + Request + "]->(target:" + User + ")\n" +
                       "WHERE target.Username = $Username\n" +
                       "RETURN " +
                       "ID(r) AS Id," +
                       "requester.Username AS Requester," +
                       "target.Username AS Target," +
                       "r.SubmitDate AS SubmitDate";
            }

            public static string GetRequestMadeByUserQuery()
            {
                return "MATCH (requester:" + User + ")-[r:" + Request + "]->(target:" + User + ")\n" +
                       "WHERE requester.Username = $Username AND ID(r) = $Request\n" +
                       "RETURN " +
                       "ID(r) AS Id," +
                       "requester.Username AS Requester," +
                       "target.Username AS Target," +
                       "r.SubmitDate AS SubmitDate";
            }

            public static string GetAllRequestsMadeByUserQuery()
            {
                return "MATCH (requester:" + User + ")-[r:" + Request + "]->(target:" + User + ")\n" +
                       "WHERE requester.Username = $Username\n" +
                       "RETURN " +
                       "ID(r) AS Id," +
                       "requester.Username AS Requester," +
                       "target.Username AS Target," +
                       "r.SubmitDate AS SubmitDate";
            }

            public static string FollowUserQuery()
            {
                return "MATCH (u1:" + User + "),(u2:" + User + ")\n" +
                       "WHERE u1.Username = $Requester AND u2.Username = $Target\n" +
                       "CREATE (u1)-[r:" + Follows + "]->(u2)\n" +
                       "SET r.SubmitDate = $SubmitDate";
            }

            public static string CreateRequestToFollowQuery()
            {
                return "MATCH (u1:" + User + "),(u2:" + User + ")\n" +
                       "WHERE u1.Username = $Requester AND u2.Username = $Target\n" +
                       "MERGE (u1)-[r:" + Request + "]->(u2)\n" +
                       "ON CREATE SET r.SubmitDate = $SubmitDate";
            }

            public static string AcceptRequestQuery()
            {
                return "MATCH (user1:" + User + ")-[r:" + Request + "]->(user2:" + User + ")\n" +
                       "WHERE ID(r) = $Request\n" +
                       "DELETE r\n" +
                       "WITH user1, user2\n" +
                       "CREATE (user1)-[:" + Follows + "]->(user2)";
            }

            public static string UnfollowUserQuery()
            {
                return "MATCH (u1:" + User + ")-[r:" + Follows + "]->(u2:" + User + ")\n" +
                       "WHERE u1.Username = $Follower AND u2.Username = $Target\n" +
                       "DELETE r";
            }

            public static string DeleteRequestQuery()
            {
                return "MATCH (:" + User + ")-[r:" + Request + "]->(:" + User + ")\n" +
                       "WHERE ID(r) = $Request\n" +
                       "DELETE r";
            }
        }

        public static class PostQueries
        {
            public static string CreatePostQuery()
            {
                return "MATCH (author:" + User + ")\n" +
                       "WHERE author.Username = $Author\n" +
                       "CREATE (author)-[:" + Posted + "]->(post:" + Post + ")\n" +
                       "SET post.UploadPath = $UploadPath\n" +
                       "SET post.Link = $Link\n" +
                       "SET post.Message = $Message\n" +
                       "SET post.SubmitDate = $SubmitDate\n" +
                       "WITH author, post\n" +
                       "RETURN " +
                       "ID(post) AS Id," +
                       "post.UploadPath AS UploadPath," +
                       "post.Link AS Link," +
                       "post.Message AS Message," +
                       "author.Username AS Author," +
                       "post.SubmitDate AS SubmitDate";
            }

            public static string CreatePostMentionsQuery()
            {
                return "MATCH (post:" + Post + ")\n" +
                       "WHERE ID(post) = $PostId\n" +
                       "WITH post\n" +
                       "UNWIND $Mentions AS mention\n" +
                       "MATCH (user:" + User + ")\n" +
                       "WHERE user.Username = mention\n" +
                       "CREATE (post)-[:" + Mentioned + "]->(user)\n" +
                       "RETURN collect(user.Username) AS Mentions";
            }

            public static string GetFeedQuery()
            {
                return "MATCH (user:" + User + "),(author:" + User + ")\n" + 
                       "WHERE author.Username = $Requester OR ( user.Username = $Requester AND EXISTS((user)-[:" + Follows + "]->(author)) )\n" +
                       "WITH author\n" +
                       "MATCH (author)-[:" + Posted + "]->(post:" + Post + ")\n" +
                       "WITH author, post\n" +
                       "OPTIONAL MATCH (up:" + User + ")-[:" + Upvoted + "]->(post)\n" +
                       "WITH author, post, collect(DISTINCT(up.Username)) AS Upvoted\n" +
                       "OPTIONAL MATCH (down:" + User + ")-[:" + Downvoted + "]->(post)\n" +
                       "WITH author, post, Upvoted, collect(DISTINCT(down.Username)) AS Downvoted\n" +
                       "WITH author, post, Upvoted, Downvoted, size(Upvoted) - size(Downvoted) AS Score\n" +
                       "OPTIONAL MATCH (post)-[:" + Mentioned + "]->(mentioned:" + User + ")\n" +
                       "WITH author, post, Upvoted, Downvoted, Score, collect(mentioned.Username) AS Mentions\n" +
                       "OPTIONAL MATCH (comment:" + Comment + ")-[:" + CommentTarget + "]->(post)\n" +
                       "WITH author, post, Upvoted, Downvoted, Score, Mentions, count(comment) AS CommentsNumber\n" +
                       "RETURN " +
                       "ID(post) AS Id," +
                       "post.UploadPath AS UploadPath," +
                       "post.Link AS Link," +
                       "post.Message AS Message," +
                       "author.Username AS Author," +
                       "post.SubmitDate AS SubmitDate," +
                       "Upvoted AS Upvoted," +
                       "Downvoted AS Downvoted," +
                       "Score AS Score," +
                       "Mentions AS Mentions," +
                       "CommentsNumber AS CommentsNumber\n" +
                       "ORDER BY post.SubmitDate DESC\n";
            }

            public static string GetAllPostsQuery()
            {
                return "MATCH (author:" + User + ")-[:" + Posted + "]->(post:" + Post + ")\n" +
                       "WITH author, post\n" +
                       "OPTIONAL MATCH (up:" + User + ")-[:" + Upvoted + "]->(post)\n" +
                       "WITH author, post, collect(DISTINCT(up.Username)) AS Upvoted\n" +
                       "OPTIONAL MATCH (down:" + User + ")-[:" + Downvoted + "]->(post)\n" +
                       "WITH author, post, Upvoted, collect(DISTINCT(down.Username)) AS Downvoted\n" +
                       "WITH author, post, Upvoted, Downvoted, size(Upvoted) - size(Downvoted) AS Score\n" +
                       "OPTIONAL MATCH (post)-[:" + Mentioned + "]->(mentioned:" + User + ")\n" +
                       "WITH author, post, Upvoted, Downvoted, Score, collect(mentioned.Username) AS Mentions\n" +
                       "OPTIONAL MATCH (comment:" + Comment + ")-[:" + CommentTarget + "]->(post)\n" +
                       "WITH author, post, Upvoted, Downvoted, Score, Mentions, count(comment) AS CommentsNumber\n" +
                       "RETURN " +
                       "ID(post) AS Id," +
                       "post.UploadPath AS UploadPath," +
                       "post.Link AS Link," +
                       "post.Message AS Message," +
                       "author.Username AS Author," +
                       "post.SubmitDate AS SubmitDate," +
                       "Upvoted AS Upvoted," +
                       "Downvoted AS Downvoted," +
                       "Score AS Score," +
                       "Mentions AS Mentions," +
                       "CommentsNumber AS CommentsNumber\n" +
                       "ORDER BY post.SubmitDate DESC\n";
            }

            public static string GetAllWatchablePostsForUserQuery()
            {
                return "MATCH (requester:" + User + ")\n" +
                       "WHERE requester.Username = $Requester\n" +
                       "WITH requester\n" +
                       "MATCH (author:" + User + ")-[:" + Posted + "]->(post:" + Post + ")\n" +
                       "WHERE (author.Privacy = 'Public') OR (author.Privacy = 'Private' AND EXISTS( (requester)-[:" + Follows + "]->(author) )) " +
                       "OR EXISTS( (post)-[:" + Mentioned + "]->(requester) )\n" +
                       "WITH author, post\n" +
                       "OPTIONAL MATCH (up:" + User + ")-[:" + Upvoted + "]->(post)\n" +
                       "WITH author, post, collect(DISTINCT(up.Username)) AS Upvoted\n" +
                       "OPTIONAL MATCH (down:" + User + ")-[:" + Downvoted + "]->(post)\n" +
                       "WITH author, post, Upvoted, collect(DISTINCT(down.Username)) AS Downvoted\n" +
                       "WITH author, post, Upvoted, Downvoted, size(Upvoted) - size(Downvoted) AS Score\n" +
                       "OPTIONAL MATCH (post)-[:" + Mentioned + "]->(mentioned:" + User + ")\n" +
                       "WITH author, post, Upvoted, Downvoted, Score, collect(mentioned.Username) AS Mentions\n" +
                       "OPTIONAL MATCH (comment:" + Comment + ")-[:" + CommentTarget + "]->(post)\n" +
                       "WITH author, post, Upvoted, Downvoted, Score, Mentions, count(comment) AS CommentsNumber\n" +
                       "RETURN " +
                       "ID(post) AS Id," +
                       "post.UploadPath AS UploadPath," +
                       "post.Link AS Link," +
                       "post.Message AS Message," +
                       "author.Username AS Author," +
                       "post.SubmitDate AS SubmitDate," +
                       "Upvoted AS Upvoted," +
                       "Downvoted AS Downvoted," +
                       "Score AS Score," +
                       "Mentions AS Mentions," +
                       "CommentsNumber AS CommentsNumber\n" +
                       "ORDER BY post.SubmitDate DESC\n";
            }

            public static string GetPostsMadeByUserQuery()
            {
                return "MATCH (author:" + User + ")-[:" + Posted + "]->(post:" + Post + ")\n" +
                       "WHERE author.Username = $Author\n" +
                       "WITH author, post\n" +
                       "OPTIONAL MATCH (up:" + User + ")-[:" + Upvoted + "]->(post)\n" +
                       "WITH author, post, collect(DISTINCT(up.Username)) AS Upvoted\n" +
                       "OPTIONAL MATCH (down:" + User + ")-[:" + Downvoted + "]->(post)\n" +
                       "WITH author, post, Upvoted, collect(DISTINCT(down.Username)) AS Downvoted\n" +
                       "WITH author, post, Upvoted, Downvoted, size(Upvoted) - size(Downvoted) AS Score\n" +
                       "OPTIONAL MATCH (post)-[:" + Mentioned + "]->(mentioned:" + User + ")\n" +
                       "WITH author, post, Upvoted, Downvoted, Score, collect(mentioned.Username) AS Mentions\n" +
                       "OPTIONAL MATCH (comment:" + Comment + ")-[:" + CommentTarget + "]->(post)\n" +
                       "WITH author, post, Upvoted, Downvoted, Score, Mentions, count(comment) AS CommentsNumber\n" +
                       "RETURN " +
                       "ID(post) AS Id," +
                       "post.UploadPath AS UploadPath," +
                       "post.Link AS Link," +
                       "post.Message AS Message," +
                       "author.Username AS Author," +
                       "post.SubmitDate AS SubmitDate," +
                       "Upvoted AS Upvoted," +
                       "Downvoted AS Downvoted," +
                       "Score AS Score," +
                       "Mentions AS Mentions," +
                       "CommentsNumber AS CommentsNumber\n" +
                       "ORDER BY post.SubmitDate DESC\n";
            }

            public static string GetPostsMentionedUserQuery()
            {
                return "MATCH (post:" + Post + ")-[:" + Mentioned + "]->(user:" + User + ")\n" +
                       "WHERE user.Username = $Username\n" +
                       "WITH post\n" +
                       "MATCH (author:" + User + ")-[:" + Posted + "]->(post)\n" +
                       "WITH author, post\n" +
                       "OPTIONAL MATCH (up:" + User + ")-[:" + Upvoted + "]->(post)\n" +
                       "WITH author, post, collect(DISTINCT(up.Username)) AS Upvoted\n" +
                       "OPTIONAL MATCH (down:" + User + ")-[:" + Downvoted + "]->(post)\n" +
                       "WITH author, post, Upvoted, collect(DISTINCT(down.Username)) AS Downvoted\n" +
                       "WITH author, post, Upvoted, Downvoted, size(Upvoted) - size(Downvoted) AS Score\n" +
                       "OPTIONAL MATCH (post)-[:" + Mentioned + "]->(mentioned:" + User + ")\n" +
                       "WITH author, post, Upvoted, Downvoted, Score, collect(mentioned.Username) AS Mentions\n" +
                       "OPTIONAL MATCH (comment:" + Comment + ")-[:" + CommentTarget + "]->(post)\n" +
                       "WITH author, post, Upvoted, Downvoted, Score, Mentions, count(comment) AS CommentsNumber\n" +
                       "RETURN " +
                       "ID(post) AS Id," +
                       "post.UploadPath AS UploadPath," +
                       "post.Link AS Link," +
                       "post.Message AS Message," +
                       "author.Username AS Author," +
                       "post.SubmitDate AS SubmitDate," +
                       "Upvoted AS Upvoted," +
                       "Downvoted AS Downvoted," +
                       "Score AS Score," +
                       "Mentions AS Mentions," +
                       "CommentsNumber AS CommentsNumber\n" +
                       "ORDER BY post.SubmitDate DESC\n";
            }

            public static string GetPostByIdQuery()
            {
                return "MATCH (author:" + User + ")-[:" + Posted + "]->(post:" + Post + ")\n" +
                       "WHERE ID(post) = $PostId\n" +
                       "WITH author, post\n" +
                       "OPTIONAL MATCH (up:" + User + ")-[:" + Upvoted + "]->(post)\n" +
                       "WITH author, post, collect(DISTINCT(up.Username)) AS Upvoted\n" +
                       "OPTIONAL MATCH (down:" + User + ")-[:" + Downvoted + "]->(post)\n" +
                       "WITH author, post, Upvoted, collect(DISTINCT(down.Username)) AS Downvoted\n" +
                       "WITH author, post, Upvoted, Downvoted, size(Upvoted) - size(Downvoted) AS Score\n" +
                       "OPTIONAL MATCH (post)-[:" + Mentioned + "]->(mentioned:" + User + ")\n" +
                       "WITH author, post, Upvoted, Downvoted, Score, collect(mentioned.Username) AS Mentions\n" +
                       "OPTIONAL MATCH (comment:" + Comment + ")-[:" + CommentTarget + "]->(post)\n" +
                       "WITH author, post, Upvoted, Downvoted, Score, Mentions, count(comment) AS CommentsNumber\n" +
                       "RETURN " +
                       "ID(post) AS Id," +
                       "post.UploadPath AS UploadPath," +
                       "post.Link AS Link," +
                       "post.Message AS Message," +
                       "author.Username AS Author," +
                       "post.SubmitDate AS SubmitDate," +
                       "Upvoted AS Upvoted," +
                       "Downvoted AS Downvoted," +
                       "Score AS Score," +
                       "Mentions AS Mentions," +
                       "CommentsNumber AS CommentsNumber";
            }

            public static string DeletePostByIdQuery()
            {
                return "MATCH (post:" + Post + ")\n" +
                       "WHERE ID(post) = $PostId\n" +
                       "DETACH DELETE post";
            }
        }

        public static class CommentQueries
        {
            public static string CreateCommentQuery()
            {
                return "MATCH (author:" + User + ")\n" +
                       "MATCH (target)\n" +
                       "WHERE author.Username = $Author AND ID(target) = $Target\n" +
                       "CREATE (author)-[:" + Commented + "]->(comment:" + Comment + ")-[:" + CommentTarget + "]->(target)\n" +
                       "SET comment.Message = $Message\n" +
                       "SET comment.SubmitDate = $SubmitDate\n" +
                       "WITH author, target, comment\n" +
                       "RETURN " +
                       "ID(comment) AS Id," +
                       "ID(target) AS Target," +
                       "comment.Message AS Message," +
                       "author.Username AS Author," +
                       "comment.SubmitDate AS SubmitDate";
            }

            public static string CreateCommentMentionsQuery()
            {
                return "MATCH (comment:" + Comment + ")\n" +
                       "WHERE ID(comment) = $CommentId\n" +
                       "WITH comment\n" +
                       "UNWIND $Mentions AS mention\n" +
                       "MATCH (user:" + User + ")\n" +
                       "WHERE user.Username = mention\n" +
                       "CREATE (comment)-[:" + Mentioned + "]->(user)\n" +
                       "RETURN collect(user.Username) AS Mentions";
            }

            public static string GetCommentsByUserQuery()
            {
                return "MATCH (author:" + User + ")-[:" + Commented + "]->(comment:" + Comment + ")\n" +
                       "MATCH (comment)-[:" + CommentTarget + "]->(target)\n" +
                       "WHERE author.Username = $Username\n" +
                       "WITH author, comment, target\n" +
                       "OPTIONAL MATCH (up:" + User + ")-[:" + Upvoted + "]->(comment)\n" +
                       "WITH author, comment, target, collect(DISTINCT(up.Username)) AS Upvoted\n" +
                       "OPTIONAL MATCH (down:" + User + ")-[:" + Downvoted + "]->(comment)\n" +
                       "WITH author, comment, target, Upvoted, collect(DISTINCT(down.Username)) AS Downvoted\n" +
                       "WITH author, comment, target, Upvoted, Downvoted, size(Upvoted) - size(Downvoted) AS Score\n" +
                       "OPTIONAL MATCH (comment)-[:" + Mentioned + "]->(mentioned:" + User + ")\n" +
                       "WITH author, comment, target, Upvoted, Downvoted, Score, collect(mentioned.Username) AS Mentions\n" +
                       "OPTIONAL MATCH (other:" + Comment + ")-[:" + CommentTarget + "]->(comment)\n" +
                       "WITH author, comment, target, Upvoted, Downvoted, Score, Mentions, count(other) AS CommentsNumber\n" +
                       "RETURN " +
                       "ID(comment) AS Id," +
                       "ID(target) AS Target," +
                       "comment.Message AS Message," +
                       "author.Username AS Author," +
                       "comment.SubmitDate AS SubmitDate," +
                       "Upvoted AS Upvoted," +
                       "Downvoted AS Downvoted," +
                       "Score AS Score," +
                       "Mentions AS Mentions," +
                       "CommentsNumber AS CommentsNumber";
            }

            public static string GetCommentsByPostQuery()
            {
                return "MATCH (author:" + User + ")-[:" + Commented + "]->(comment:" + Comment + ")\n" +
                       "MATCH (comment)-[:" + CommentTarget + "]->(target:" + Post + ")\n" +
                       "WHERE ID(target) = $PostId\n" +
                       "WITH author, comment, target\n" +
                       "OPTIONAL MATCH (up:" + User + ")-[:" + Upvoted + "]->(comment)\n" +
                       "WITH author, comment, target, collect(DISTINCT(up.Username)) AS Upvoted\n" +
                       "OPTIONAL MATCH (down:" + User + ")-[:" + Downvoted + "]->(comment)\n" +
                       "WITH author, comment, target, Upvoted, collect(DISTINCT(down.Username)) AS Downvoted\n" +
                       "WITH author, comment, target, Upvoted, Downvoted, size(Upvoted) - size(Downvoted) AS Score\n" +
                       "OPTIONAL MATCH (comment)-[:" + Mentioned + "]->(mentioned:" + User + ")\n" +
                       "WITH author, comment, target, Upvoted, Downvoted, Score, collect(mentioned.Username) AS Mentions\n" +
                       "OPTIONAL MATCH (other:" + Comment + ")-[:" + CommentTarget + "]->(comment)\n" +
                       "WITH author, comment, target, Upvoted, Downvoted, Score, Mentions, count(other) AS CommentsNumber\n" +
                       "RETURN " +
                       "ID(comment) AS Id," +
                       "ID(target) AS Target," +
                       "comment.Message AS Message," +
                       "author.Username AS Author," +
                       "comment.SubmitDate AS SubmitDate," +
                       "Upvoted AS Upvoted," +
                       "Downvoted AS Downvoted," +
                       "Score AS Score," +
                       "Mentions AS Mentions," +
                       "CommentsNumber AS CommentsNumber";
            }

            public static string GetCommentsByCommentQuery()
            {
                return "MATCH (author:" + User + ")-[:" + Commented + "]->(comment:" + Comment + ")\n" +
                       "MATCH (comment)-[:" + CommentTarget + "]->(target:" + Comment + ")\n" +
                       "WHERE ID(target) = $CommentId\n" +
                       "WITH author, comment, target\n" +
                       "OPTIONAL MATCH (up:" + User + ")-[:" + Upvoted + "]->(comment)\n" +
                       "WITH author, comment, target, collect(DISTINCT(up.Username)) AS Upvoted\n" +
                       "OPTIONAL MATCH (down:" + User + ")-[:" + Downvoted + "]->(comment)\n" +
                       "WITH author, comment, target, Upvoted, collect(DISTINCT(down.Username)) AS Downvoted\n" +
                       "WITH author, comment, target, Upvoted, Downvoted, size(Upvoted) - size(Downvoted) AS Score\n" +
                       "OPTIONAL MATCH (comment)-[:" + Mentioned + "]->(mentioned:" + User + ")\n" +
                       "WITH author, comment, target, Upvoted, Downvoted, Score, collect(mentioned.Username) AS Mentions\n" +
                       "OPTIONAL MATCH (other:" + Comment + ")-[:" + CommentTarget + "]->(comment)\n" +
                       "WITH author, comment, target, Upvoted, Downvoted, Score, Mentions, count(other) AS CommentsNumber\n" +
                       "RETURN " +
                       "ID(comment) AS Id," +
                       "ID(target) AS Target," +
                       "comment.Message AS Message," +
                       "author.Username AS Author," +
                       "comment.SubmitDate AS SubmitDate," +
                       "Upvoted AS Upvoted," +
                       "Downvoted AS Downvoted," +
                       "Score AS Score," +
                       "Mentions AS Mentions," +
                       "CommentsNumber AS CommentsNumber";
            }

            public static string GetCommentByIdQuery()
            {
                return "MATCH (author:" + User + ")-[:" + Commented + "]->(comment:" + Comment + ")\n" +
                       "MATCH (comment)-[:" + CommentTarget + "]->(target)\n" +
                       "WHERE ID(comment) = $CommentId\n" +
                       "WITH author, comment, target\n" +
                       "OPTIONAL MATCH (up:" + User + ")-[:" + Upvoted + "]->(comment)\n" +
                       "WITH author, comment, target, collect(DISTINCT(up.Username)) AS Upvoted\n" +
                       "OPTIONAL MATCH (down:" + User + ")-[:" + Downvoted + "]->(comment)\n" +
                       "WITH author, comment, target, Upvoted, collect(DISTINCT(down.Username)) AS Downvoted\n" +
                       "WITH author, comment, target, Upvoted, Downvoted, size(Upvoted) - size(Downvoted) AS Score\n" +
                       "OPTIONAL MATCH (comment)-[:" + Mentioned + "]->(mentioned:" + User + ")\n" +
                       "WITH author, comment, target, Upvoted, Downvoted, Score, collect(mentioned.Username) AS Mentions\n" +
                       "OPTIONAL MATCH (other:" + Comment + ")-[:" + CommentTarget + "]->(comment)\n" +
                       "WITH author, comment, target, Upvoted, Downvoted, Score, Mentions, count(other) AS CommentsNumber\n" +
                       "RETURN " +
                       "ID(comment) AS Id," +
                       "ID(target) AS Target," +
                       "comment.Message AS Message," +
                       "author.Username AS Author," +
                       "comment.SubmitDate AS SubmitDate," +
                       "Upvoted AS Upvoted," +
                       "Downvoted AS Downvoted," +
                       "Score AS Score," +
                       "Mentions AS Mentions," +
                       "CommentsNumber AS CommentsNumber";
            }

            public static string DeleteCommentByIdQuery()
            {
                return "MATCH (comment:" + Comment + ")\n" +
                       "WHERE ID(comment) = $CommentId\n" +
                       "DETACH DELETE comment"; 
            }
        }

        public static class ReactionQueries
        {
            public static string DidYouVoteQuery()
            {
                return "MATCH (user:" + User + ")-[r:" + Upvoted + "|" + Downvoted + "]->(target)\n" +
                       "WHERE user.Username = $Username AND ID(target) = $Id\n" +
                       "RETURN count(r) > 0 AS DidYouVote";
            }

            public static string DidYouUpvoteQuery()
            {
                return "MATCH (user:" + User + ")-[r:" + Upvoted + "]->(target)\n" +
                       "WHERE user.Username = $Username AND ID(target) = $Id\n" +
                       "RETURN count(r) > 0 AS DidYouUpvote";
            }

            public static string DidYouDownvoteQuery()
            {
                return "MATCH (user:" + User + ")-[r:" + Downvoted + "]->(target)\n" +
                       "WHERE user.Username = $Username AND ID(target) = $Id\n" +
                       "RETURN count(r) > 0 AS DidYouDownvote";
            }

            public static string UpvoteQuery()
            {
                return "MATCH (user:" + User + "),(target)\n" +
                       "WHERE user.Username = $Username AND ID(target) = $Id\n" +
                       "OPTIONAL MATCH (user)-[r:" + Downvoted + "]->(target)\n" +
                       "DELETE r\n" +
                       "WITH user, target\n" +
                       "CREATE (user)-[:" + Upvoted + "]->(target)\n";
            }

            public static string DownvoteQuery()
            {
                return "MATCH (user:" + User + "),(target)\n" +
                       "WHERE user.Username = $Username AND ID(target) = $Id\n" +
                       "OPTIONAL MATCH (user)-[r:" + Upvoted + "]->(target)\n" +
                       "DELETE r\n" +
                       "WITH user, target\n" +
                       "CREATE (user)-[:" + Downvoted + "]->(target)\n";
            }

            public static string RemoveVoteQuery()
            {
                return "MATCH (user:" + User + ")-[r:" + Upvoted + "|" + Downvoted + "]->(target)\n" +
                       "WHERE user.Username = $Username AND ID(target) = $Id\n" +
                       "DELETE r\n";

            }

            public static string GetPostsReactedByUserQuery()
            {
                return "MATCH (user:" + User + ")-[:" + Upvoted + "|" + Downvoted + "]->(post:" + Post + ")\n" +
                       "WHERE user.Username = $Username\n" +
                       "WITH post\n" +
                       "MATCH (author:" + User + ")-[:" + Posted + "]->(post)\n" +
                       "WITH author, post\n" +
                       "OPTIONAL MATCH (up:" + User + ")-[:" + Upvoted + "]->(post)\n" +
                       "WITH author, post, collect(DISTINCT(up.Username)) AS Upvoted\n" +
                       "OPTIONAL MATCH (down:" + User + ")-[:" + Downvoted + "]->(post)\n" +
                       "WITH author, post, Upvoted, collect(DISTINCT(down.Username)) AS Downvoted\n" +
                       "WITH author, post, Upvoted, Downvoted, size(Upvoted) - size(Downvoted) AS Score\n" +
                       "OPTIONAL MATCH (post)-[:" + Mentioned + "]->(mentioned:" + User + ")\n" +
                       "WITH author, post, Upvoted, Downvoted, Score, collect(mentioned.Username) AS Mentions\n" +
                       "OPTIONAL MATCH (comment:" + Comment + ")-[:" + Commented + "]->(post)\n" +
                       "WITH author, post, Upvoted, Downvoted, Score, Mentions, count(comment) AS CommentsNumber\n" +
                       "RETURN " +
                       "ID(post) AS Id," +
                       "post.UploadPath AS UploadPath," +
                       "post.Link AS Link," +
                       "post.Message AS Message," +
                       "author.Username AS Author," +
                       "post.SubmitDate AS SubmitDate," +
                       "Upvoted AS Upvoted," +
                       "Downvoted AS Downvoted," +
                       "Score AS Score," +
                       "Mentions AS Mentions," +
                       "CommentsNumber AS CommentsNumber\n" +
                       "ORDER BY post.SubmitDate DESC\n";
            }

            public static string GetCommentsReactedByUserQuery()
            {
                return "MATCH (user:" + User + ")-[:" + Upvoted + "|" + Downvoted + "]->(comment:" + Comment + ")\n" +
                       "WHERE user.Username = $Username\n" +
                       "WITH comment\n" +
                       "MATCH (author:" + User + ")-[:" + Commented + "]->(comment)\n" +
                       "MATCH (comment)-[:" + CommentTarget + "]->(target)\n" +
                       "WITH author, comment, target\n" +
                       "OPTIONAL MATCH (up:" + User + ")-[:" + Upvoted + "]->(comment)\n" +
                       "WITH author, comment, target, collect(DISTINCT(up.Username)) AS Upvoted\n" +
                       "OPTIONAL MATCH (down:" + User + ")-[:" + Downvoted + "]->(comment)\n" +
                       "WITH author, comment, target, Upvoted, collect(DISTINCT(down.Username)) AS Downvoted\n" +
                       "WITH author, comment, target, Upvoted, Downvoted, size(Upvoted) - size(Downvoted) AS Score\n" +
                       "OPTIONAL MATCH (comment)-[:" + Mentioned + "]->(mentioned:" + User + ")\n" +
                       "WITH author, comment, target, Upvoted, Downvoted, Score, collect(mentioned.Username) AS Mentions\n" +
                       "OPTIONAL MATCH (other:" + Comment + ")-[:" + Commented + "]->(comment)\n" +
                       "WITH author, comment, target, Upvoted, Downvoted, Score, Mentions, count(other) AS CommentsNumber\n" +
                       "RETURN " +
                       "ID(comment) AS Id," +
                       "ID(target) AS Target," +
                       "comment.Message AS Message," +
                       "author.Username AS Author," +
                       "comment.SubmitDate AS SubmitDate," +
                       "Upvoted AS Upvoted," +
                       "Downvoted AS Downvoted," +
                       "Score AS Score," +
                       "Mentions AS Mentions," +
                       "CommentsNumber AS CommentsNumber";
            }
        }

        public static class NotificationQueries
        {
            public static string CreateNotificationQuery()
            {
                return "MATCH (target:" + User + "),(author:" + User + ")\n" +
                       "WHERE target.Username = $Target AND author.Username = $Author\n" +
                       "WITH target, author\n" +
                       "CREATE (target)<-[:" + Notifies + "]-" +
                       "(n:" + Notification + "{ Type: $Type, SubmitDate: $SubmitDate, AlreadySeen: $AlreadySeen })" +
                       "-[:" + Responsible + "]->(author)\n" +
                       "RETURN " +
                       "ID(n) AS Id," +
                       "n.Type AS Type," +
                       "author.Username AS Author," +
                       "target.Username AS Target," +
                       "n.SubmitDate AS SubmitDate," +
                       "n.AlreadySeen AS AlreadySeen";
            }

            public static string RelatedNotificationToPostQuery()
            {
                return "MATCH (n:" + Notification + "),(post:" + Post + ")\n" +
                       "WHERE ID(n) = $NotificationId AND ID(post) = $PostId\n" +
                       "CREATE (n)-[:" + RelatedToPost + "]->(post)\n" +
                       "RETURN ID(post) AS Id";
            }

            public static string RelatedNotificationToCommentQuery()
            {
                return "MATCH (n:" + Notification + "),(comment:" + Comment + ")\n" +
                       "WHERE ID(n) = $NotificationId AND ID(comment) = $CommentId\n" +
                       "CREATE (n)-[:" + RelatedToComment + "]->(comment)\n" +
                       "RETURN ID(comment) AS Id";
            }

            public static string RelatedNotificationToAnswerQuery()
            {
                return "MATCH (n:" + Notification + "),(comment:" + Comment + ")\n" +
                       "WHERE ID(n) = $NotificationId AND ID(comment) = $CommentId\n" +
                       "CREATE (n)-[:" + RelatedToAnswer + "]->(comment)\n" +
                       "RETURN ID(comment) AS Id";
            }

            public static string GetNotificationsForUserQuery()
            {
                return "MATCH (target)<-[:" + Notifies + "]-(n:" + Notification + ")-[:" + Responsible + "]->(author)\n" +
                       "WHERE target.Username = $Target\n" +
                       "WITH target, author, n\n" +
                       "OPTIONAL MATCH (n)-[:" + RelatedToPost + "]->(post:" + Post + ")\n" +
                       "OPTIONAL MATCH (n)-[:" + RelatedToComment + "]->(comment:" + Comment + ")\n" +
                       "OPTIONAL MATCH (n)-[:" + RelatedToAnswer + "]->(answer:" + Comment + ")\n" +
                       "RETURN " +
                       "ID(n) AS Id," +
                       "n.Type AS Type," +
                       "author.Username AS Author," +
                       "target.Username AS Target," +
                       "ID(post) AS PostId," +
                       "ID(comment) AS CommentId," +
                       "ID(answer) AS AnswerId," +
                       "n.SubmitDate AS SubmitDate," +
                       "n.AlreadySeen AS AlreadySeen\n" +
                       "ORDER BY n.SubmitDate DESC";
            }

            public static string GetNotificationByIdQuery()
            {
                return "MATCH (target)<-[:" + Notifies + "]-(n:" + Notification + ")-[:" + Responsible + "]->(author)\n" +
                       "WHERE ID(n) = $Id\n" +
                       "WITH target, author, n\n" +
                       "OPTIONAL MATCH (n)-[:" + RelatedToPost + "]->(post:" + Post + ")\n" +
                       "OPTIONAL MATCH (n)-[:" + RelatedToComment + "]->(comment:" + Comment + ")\n" +
                       "OPTIONAL MATCH (n)-[:" + RelatedToAnswer + "]->(answer:" + Comment + ")\n" +
                       "RETURN " +
                       "ID(n) AS Id," +
                       "n.Type AS Type," +
                       "author.Username AS Author," +
                       "target.Username AS Target," +
                       "ID(post) AS PostId," +
                       "ID(comment) AS CommentId," +
                       "ID(answer) AS AnswerId," +
                       "n.SubmitDate AS SubmitDate," +
                       "n.AlreadySeen AS AlreadySeen";
            }

            public static string UpdateNotificationsQuery()
            {
                return "MATCH (target:" + User + ")<-[:" + Notifies + "]-(n:" + Notification + ")-[:" + Responsible + "]->(author:" + User + ")\n" +
                       "WHERE target.Username = $Target\n" +
                       "SET n.AlreadySeen = $Seen\n" +
                       "WITH target, author, n\n" +
                       "OPTIONAL MATCH (n)-[:" + RelatedToPost + "]->(post:" + Post + ")\n" +
                       "OPTIONAL MATCH (n)-[:" + RelatedToComment + "]->(comment:" + Comment + ")\n" +
                       "OPTIONAL MATCH (n)-[:" + RelatedToAnswer + "]->(answer:" + Comment + ")\n" +
                       "RETURN " +
                       "ID(n) AS Id," +
                       "n.Type AS Type," +
                       "author.Username AS Author," +
                       "target.Username AS Target," +
                       "ID(post) AS PostId," +
                       "ID(comment) AS CommentId," +
                       "ID(answer) AS AnswerId," +
                       "n.SubmitDate AS SubmitDate," +
                       "n.AlreadySeen AS AlreadySeen\n" +
                       "ORDER BY n.SubmitDate DESC";
            }

            public static string UpdateNotificationByIdQuery()
            {
                return "MATCH (target:" + User + ")<-[:" + Notifies + "]-(n:" + Notification + ")-[:" + Responsible + "]->(author:" + User + ")\n" +
                       "WHERE ID(n) = $NotificationId\n" +
                       "SET n.AlreadySeen = $Seen\n" +
                       "WITH target, author, n\n" +
                       "OPTIONAL MATCH (n)-[:" + RelatedToPost + "]->(post:" + Post + ")\n" +
                       "OPTIONAL MATCH (n)-[:" + RelatedToComment + "]->(comment:" + Comment + ")\n" +
                       "OPTIONAL MATCH (n)-[:" + RelatedToAnswer + "]->(answer:" + Comment + ")\n" +
                       "RETURN " +
                       "ID(n) AS Id," +
                       "n.Type AS Type," +
                       "author.Username AS Author," +
                       "target.Username AS Target," +
                       "ID(post) AS PostId," +
                       "ID(comment) AS CommentId," +
                       "ID(answer) AS AnswerId," +
                       "n.SubmitDate AS SubmitDate," +
                       "n.AlreadySeen AS AlreadySeen\n" +
                       "ORDER BY n.SubmitDate DESC";
            }

            public static string DeleteAllNotificationsOfUserQuery()
            {
                return "MATCH (target)<-[:" + Notifies + "]-(n:" + Notification + ")-[:" + Responsible + "]->(author)\n" +
                       "WHERE target.Username = $Target\n" +
                       "DETACH DELETE n";
            }

            public static string DeleteNotificationByIdQuery()
            {
                return "MATCH (n:" + Notification + ")\n" +
                       "WHERE ID(n) = $Id\n" +
                       "DETACH DELETE n";
            }
        }
    }
}
