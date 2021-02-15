using System.IO;

namespace WriteAndShareWebApi.Enums
{
    public static class Uploads
    {
        private const string Upload = "Uploads";
        private const string Profile = "Profile";
        private const string Header = "Header";
        private const string Avatar = "Avatar";
        private const string Publications = "Publications";

        public const string DefaultHeader = "default_header.jpg";
        public const string DefaultAvatar = "default_avatar.jpg";

        public static string GetHeaderFolderPath()
        {
            return Path.Combine(Path.Combine(Upload, Profile), Header);
        }

        public static string GetAvatarFolderPath()
        {
            return Path.Combine(Path.Combine(Upload, Profile), Avatar);
        }

        public static string GetPublicationsFolderPath()
        {
            return Path.Combine(Upload, Publications);
        }
    }
}
