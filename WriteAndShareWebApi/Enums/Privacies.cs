namespace WriteAndShareWebApi.Enums
{
    public static class Privacies
    {
        public const string Public = "Public";
        public const string Private = "Private";
        public const string Desactivated = "Desactivated";

        public static bool IsPrivacyValid(string privacy)
        {
            if (Public == privacy) return true;
            if (Private == privacy) return true;
            return false;
        }
    }
}
