using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

partial class Program
{
    public static void Main(string[] args)
    {
        foreach (var path in args)
        {
            AnalyzeCacheFile(path);
        }
    }

    public static void AnalyzeCacheFile(string path)
    {
        using var file = File.OpenRead(path);
        var fileType = GetFileType(file);

        Console.WriteLine($"{path}: {fileType}");
    }

    public static string GetFileType(FileStream file)
    {
        var treeNodeType = GetTreeNodeType(file);
        if (file.Length == 0)
        {
            return "Empty";
        }
        else if (IsAssetBundle(file))
        {
            return "AssetBundle";
        }
        else if (IsJson(file))
        {
            return "JSON";
        }
        else if (IsPng(file))
        {
            return "PNG";
        }
        else if (treeNodeType != null)
        {
            return $"TreeNode ({treeNodeType})";
        }
        else
        {
            return "Unknown";
        }
    }

    public static bool IsAssetBundle(FileStream file)
    {
        using var reader = new BinaryReader(file, Encoding.UTF8, leaveOpen: true);

        var magic = reader.ReadBytes(7);
        var fileType = Encoding.UTF8.GetString(magic);

        file.Position = 0;

        return fileType == "UnityFS"
        || fileType == "UnityWe"; // UnityWeb
    }

    public static bool IsJson(FileStream file)
    {
        using var reader = new BinaryReader(file, Encoding.UTF8, leaveOpen: true);

        var character = reader.ReadChar();

        file.Position = 0;

        return character == '{';
    }

    public static bool IsPng(FileStream file)
    {
        using var reader = new BinaryReader(file, Encoding.UTF8, leaveOpen: true);

        var magic = reader.ReadBytes(4);

        file.Position = 0;

        return magic[0] == 0x89
            && magic[1] == 0x50
            && magic[2] == 0x4E
            && magic[3] == 0x47;
    }

    private static Regex treeNodeRegex = new(@"^\w+$", RegexOptions.Singleline);

    public static string GetTreeNodeType(FileStream file)
    {
        if (file.Length < 10)
        {
            return null;
        }
        using var reader = new BinaryReader(file, Encoding.UTF8, leaveOpen: true);

        // Read 32 bytes to determine the file type
        var header = reader.ReadBytes(32);
        file.Position = 0;

        var hasBom = header[0] == 0xEF && header[1] == 0xBB && header[2] == 0xBF;
        var offset = hasBom ? 3 : 0;

        if (header.Length < 32)
        {
            return null;
        }

        var headerString = Encoding.UTF8.GetString(header, offset, 32 - offset);
        var fileType = headerString.Split("\r\n")[0].Trim();

        if (!treeNodeRegex.IsMatch(fileType))
        {
            return null;
        }

        return fileType;
    }
}
