using System.IO;

public static class FileUtilities
{
	public static void CreateOrOverwriteAllText(string filePath, string content)
	{
		string directory = Path.GetDirectoryName(filePath);

		if (!Directory.Exists(directory))
		{
			Directory.CreateDirectory(directory);
		}

		File.WriteAllText(filePath, content);
	}
}