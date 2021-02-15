namespace WriteAndShareWebApi.Models
{
    public static class Roles
    {
        public const string Administrator = "Administrator";
        public const string User = "User";
        public const string Desactivated = "Desactivated";

        public static bool IsRoleValid(string role)
        {
            if (Administrator == role) return true;
            if (User == role) return true;
            return false;
        }
    }
}
