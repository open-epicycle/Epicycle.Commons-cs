namespace Epicycle.Commons.FileSystem
{
    public static class FileSystemPathUtils
    {
        public static string SanitizePathString(string unsanitizedString)
        {
            var forbiddenChars = new char[] {'<', '>', ':', '"', '/', '\\', '|', '?', '*'};

            var result = unsanitizedString;
            foreach(var forbiddenChar in forbiddenChars)
            {
                result = result.Replace(forbiddenChar, '_');
            }

            return result;
        }
    }
}
