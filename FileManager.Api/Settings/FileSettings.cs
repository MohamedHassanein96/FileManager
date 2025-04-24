namespace FileManager.Api.Settings
{
    public static class FileSettings
    {
        public const int MaxFileSizeInMB = 1;
        public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
        public static readonly string[] bLockedSignature = ["4D-5A", "2F-2A", "D0-CF"];
        public static readonly string[] AllowedImagesExtensions = [".jpg", ".jpeg", ".png"];
        public static readonly string AllowedPattern = "^[A-Za-z0-9_\\-.]*$";
    }
}
