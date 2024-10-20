using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityDataTools.FileSystem;
using UnityDataTools.TextDumper;

partial class Program
{
    private static TextDumperTool textDumperTool = new();

    public static void Main(string[] args)
    {
        try
        {
            UnityFileSystem.Init();
            // Create temp directory for extracted files
            Directory.CreateDirectory("temp");
            foreach (var path in args)
            {
                AnalyzeCacheFile(path);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            UnityFileSystem.Cleanup();
            // We will likely want to manually inspect files right now, so we will not delete the temp directory
            // Directory.Delete("temp", recursive: true);
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
        var jsonType = GetJsonType(file);
        var bundleType = GetBundleType(file);
        if (file.Length == 0)
        {
            return "Empty";
        }
        else if (bundleType != BundleType.None)
        {
            return bundleType.ToString();
        }
        else if (jsonType != null)
        {
            return $"JSON ({jsonType})";
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

    public enum BundleType
    {
        AssetBundle,
        WebBundle,
        None,
    }

    public static BundleType GetBundleType(FileStream file)
    {
        if (file.Length < 7)
        {
            return BundleType.None;
        }
        using var reader = new BinaryReader(file, Encoding.UTF8, leaveOpen: true);

        var magic = reader.ReadBytes(7);
       file.Position = 0;
        var fileType = Encoding.UTF8.GetString(magic);

        if (fileType != "UnityFS" && fileType != "UnityWe")
        {
            return BundleType.None;
        }

        // Create directory at temp/$filename
        var outputFolder = Path.Combine("temp", Path.GetFileName(file.Name));
        Directory.CreateDirectory(outputFolder);
        using var files = UnityFileSystem.MountArchive(file.Name, "/");
        foreach (var f in files.Nodes)
        {
            if (!f.Flags.HasFlag(ArchiveNodeFlags.SerializedFile))
            {
                Console.WriteLine($"Extracting {outputFolder}/{f.Path}");
                using var sourceFile = UnityFileSystem.OpenFile("/" + f.Path);
                using var destFile = File.OpenWrite(Path.Combine(outputFolder, f.Path));
                const int blockSize = 256 * 1024;
                var buffer = new byte[blockSize];
                long actualSize;

                do
                {
                    actualSize = sourceFile.Read(blockSize, buffer);
                    destFile.Write(buffer, 0, (int)actualSize);
                }
                while (actualSize == blockSize);
            }
            else
            {
                Console.WriteLine($"Dumping {outputFolder}/{f.Path}");
                var status = textDumperTool.Dump("/" + f.Path, outputFolder, false);
                if (status != 0)
                {
                    Console.WriteLine($"Error dumping {f.Path}");
                    return BundleType.None;
                }
            }
        }

        return BundleType.AssetBundle;
    }

    public static string GetJsonType(FileStream file)
    {
        if (file.Length < 2)
        {
            return null;
        }
        using var reader = new BinaryReader(file, Encoding.UTF8, leaveOpen: true);

        Console.WriteLine(file.Name);
        var character = reader.ReadChar();

        file.Position = 0;

        if (character != '{')
        {
            return null;
        }

        using var streamReader = new StreamReader(file, Encoding.UTF8, leaveOpen: true);
        var content = streamReader.ReadToEnd();
        file.Position = 0;

        // In the future we should use a parser to determine the type,
        // however it seems that a lot of the JSON is straight up invalid
        // and actually half TreeNodes, so it will require adding in the
        // JSON fallback behavior to our implementation.

        if (content.Contains("\"Months\""))
        {
            return "WalletStoreItem";
        }
        else if (content.Contains("\"Collection_Name\""))
        {
            return "ShopProductItem";
        }
        else if (content.Contains("\"ScreenOffset\""))
        {
            return "NpcDetails";
        }
        else if (content.Contains("\"Name\""))
        {
            return "ItemDetails";
        }

        return "Unknown";
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

        using var streamReader = new StreamReader(file, Encoding.UTF8, leaveOpen: true);
        var content = streamReader.ReadToEnd();
        file.Position = 0;

        TreeNode.Object treeNode;
        try
        {
            treeNode = TreeNode.Read(content);
        }
        catch (CorruptException)
        {
            return "Corrupt";
        }

        if (knownTypes.Contains(treeNode.Name))
        {
            return treeNode.Name;
        }
        else if (treeNode.Children.Count > 0
            && treeNode.Children[0] is TreeNode.Object obj
            && knownRootTypes.Contains(obj.Name)
        )
        {
            return obj.Name;
        }
        else if (treeNode.Children.Exists(
            child => child is TreeNode.Int intNode
            && intNode.Name == "TimeoutLength"
        ))
        {
            return "GameConfig";
        }
        else if (treeNode.Children.Exists(
            child => child is TreeNode.String stringNode
            && captionNameRegex.IsMatch(stringNode.Name)
        ))
        {
            return "Caption";
        }
        else if (treeNode.Children.Count > 0
            && treeNode.Children[0] is TreeNode.Object objNode
            && objNode.Children.Exists(
                child => child is TreeNode.String stringNode
                && stringNode.Name == "onMouseClick"
            )
        )
        {
            return "ButtonSoundMapping";
        }


        Console.WriteLine(fileType);

        return null;
    }

    private static HashSet<string> knownTypes = new()
    {
        "Quest",
        "NPCRelationships",
        "Item",
        "DressAvatarSlots",
        "NPCAnimations",
        "Mission",
        "BuildingCompletion",
        "Property",
        "BuildingUI",
        "SpawnPoints",
        "Areas",
        "AvatarProperty",
        "NPCs",
    };

    private static HashSet<string> knownRootTypes = new()
    {
        "UI",
        "Localization",
        "Tooltip",
        "Nix",
        "Emotes",
    };

    private static Regex captionNameRegex = new(@"^C(\w+)\d+$");
}
