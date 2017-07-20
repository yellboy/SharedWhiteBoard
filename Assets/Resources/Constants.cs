namespace Assets.Resources
{
    public static class Constants
    {
        public const string ApplicationUrl = "http://distributedwhiteboard.azurewebsites.net";

        public const string ConnectToExistingSessionUrl = "{0}/SessionApi/Session/Pin{1}";

        public const string EndSessionUrl = "{0}/SessionApi/Session/Pin/{1}";

        public const string GetImageUrl = "{0}/ImageApi/Image/{1}";

        public const string GetDarkImageUrl = "{0}/ImageApi/Image/{1}/Dark";

        public const string ImageUploadUrl = "{0}/ImageApi/Image/{1}";

        public const string StartSessionUrl = "{0}/SessionApi/Session";
    }
}
