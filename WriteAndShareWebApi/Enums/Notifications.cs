namespace WriteAndShareWebApi.Enums
{
    public static class Notifications
    {
        public const string UserFollowed = "UF";
        public const string UserAcceptedRequest = "UAR";

        public const string UserMentionedInPost = "UMIP";
        public const string UserUpvotedThePost = "UUTP";
        public const string UserDownvotedThePost = "UDTP";
        public const string UserCommentedThePost = "UCTP";

        public const string UserMentionedInComment = "UMIC";
        public const string UserUpvotedTheComment = "UUTC";
        public const string UserDownvotedTheComment = "UDTC";
        public const string UserCommentedTheComment = "UCTC";

        public const int UndefinedPostId = -99;
        public const int UndefinedCommentId = -99;
        public const int UndefinedAnswerId = -99;
    }
}
