using System.IO;

public static class FileUtilities
{
    public static bool CreateOrOverwriteAllText(string filePath, string content)
    {
        string directory = Path.GetDirectoryName(filePath);

        if (!Directory.Exists(directory))
        {
            try
            {
                Directory.CreateDirectory(directory);
            }
            catch
            {
                return false;
            }
        }

        File.WriteAllText(filePath, content);
        return true;
    }

    public static string GetFilepathWithTimestamp(string filename)
    {
        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");

        return string.Format("{0}_{1}", filename, timestamp);
    }
}