using System;
namespace userAPI.Utilities
{
    public static class Utilities
    {
        public static IFormFile GetIFormFileFromPath(string filePath)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException($"File not found at path: {filePath}");

            using var stream = new FileStream(filePath, FileMode.Open);

            var formFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(filePath))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream"
            };

            return formFile;
        }
    }
}

