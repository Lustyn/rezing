using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using UnityDataTools.FileSystem;
using UnityDataTools.TextDumper;

partial class Program
{
    private TextDumperTool textDumperTool = new();

    public static void Main(string[] args)
    {
        Console.WriteLine("rezing CacheTool");
        try
        {
            UnityFileSystem.Init();
            // Create temp directory for extracted files
            Directory.CreateDirectory("temp");
            var program = new Program();
            program.LoadCacheItems();
            foreach (var path in args)
            {
                try
                {
                    program.AnalyzeCacheFile(path);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error analyzing {path}: {e}");
                }
            }
            program.SaveCacheItems();
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

    private Dictionary<string, CacheItem> cacheItems = new();

    public void LoadCacheItems()
    {
        if (!Directory.Exists("data"))
        {
            Directory.CreateDirectory("data");
        }

        // Load cache items from data/cache.json
        if (File.Exists("data/cache.json"))
        {
            var cacheJson = File.ReadAllText("data/cache.json");
            cacheItems = JsonSerializer.Deserialize<Dictionary<string, CacheItem>>(cacheJson);
            Console.WriteLine($"Loaded {cacheItems.Count} existing cache items");
        }
    }

    public void SaveCacheItems()
    {
        var cacheJson = JsonSerializer.Serialize(cacheItems, new JsonSerializerOptions
        {
            WriteIndented = true,
        });
        File.WriteAllText("data/cache.json", cacheJson);
        Console.WriteLine($"Saved {cacheItems.Count} cache items");
    }

    public static string PadBase64(string base64)
    {
        return base64.PadRight(base64.Length / 4 * 4 + (base64.Length % 4 == 0 ? 0 : 4), '=');
    }

    public static bool IsBase64String(string base64)
    {
        Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
        return Convert.TryFromBase64String(
            PadBase64(base64),
            buffer,
            out _
        );
    }

    public void AnalyzeCacheFile(string path)
    {
        var fileName = Path.GetFileName(path).Trim();
        // Skip if filename is not a base64 encoded OID
        if (!IsBase64String(fileName))
        {
            Console.WriteLine($"Skipping {fileName}");
            return;
        }
        var oid = Encoding.UTF8.GetString(Convert.FromBase64String(PadBase64(fileName)));

        using var file = File.OpenRead(path);
        var cacheItem = cacheItems.GetValueOrDefault(fileName);
        UpdateCacheItem(cacheItem, file);

        if (!cacheItems.ContainsKey(fileName))
        {
            cacheItems[fileName] = cacheItem;
            Console.WriteLine($"New cache item: {fileName} ({cacheItem.FileType})");
        }
        else if (cacheItems[fileName] != cacheItem)
        {
            Console.WriteLine($"Updated cache item: {fileName} ({cacheItem.FileType})");
        }
    }

    public void UpdateCacheItem(CacheItem cacheItem, FileStream file)
    {
        var treeNodeType = GetTreeNodeType(file);
        var jsonType = GetJsonType(file);
        var bundleType = GetBundleType(file);
        if (file.Length == 0)
        {
            cacheItem.FileType = "Empty";
            cacheItem.Type = null;
        }
        else if (bundleType != BundleType.None)
        {
            cacheItem.FileType = bundleType.ToString();
            cacheItem.Type = null;
        }
        else if (jsonType != null)
        {
            cacheItem.FileType = "JSON";
            cacheItem.Type = jsonType;
        }
        else if (IsPng(file))
        {
            cacheItem.FileType = "PNG";
            cacheItem.Type = null;
        }
        else if (treeNodeType != null)
        {
            cacheItem.FileType = "TreeNode";
            cacheItem.Type = treeNodeType;
        }
        else if (IsAudioFile(file))
        {
            cacheItem.FileType = "Audio";
            cacheItem.Type = null;
        }
        else
        {
            cacheItem.FileType = "Unknown";
            cacheItem.Type = null;
        }
    }

    public enum BundleType
    {
        AssetBundle,
        WebBundle,
        None,
    }

    public BundleType GetBundleType(FileStream file)
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
        // var outputFolder = Path.Combine("temp", Path.GetFileName(file.Name));
        // Directory.CreateDirectory(outputFolder);
        // using var files = UnityFileSystem.MountArchive(file.Name, "/");
        // foreach (var f in files.Nodes)
        // {
        //     if (!f.Flags.HasFlag(ArchiveNodeFlags.SerializedFile))
        //     {
        //         Console.WriteLine($"Extracting {outputFolder}/{f.Path}");
        //         using var sourceFile = UnityFileSystem.OpenFile("/" + f.Path);
        //         using var destFile = File.OpenWrite(Path.Combine(outputFolder, f.Path));
        //         const int blockSize = 256 * 1024;
        //         var buffer = new byte[blockSize];
        //         long actualSize;
        //         do
        //         {
        //             actualSize = sourceFile.Read(blockSize, buffer);
        //             destFile.Write(buffer, 0, (int)actualSize);
        //         }
        //         while (actualSize == blockSize);
        //     }
        //     else
        //     {
        //         Console.WriteLine($"Dumping {outputFolder}/{f.Path}");
        //         var status = textDumperTool.Dump("/" + f.Path, outputFolder, false);
        //         if (status != 0)
        //         {
        //             Console.WriteLine($"Error dumping {f.Path}");
        //             return BundleType.None;
        //         }
        //     }
        // }

        return BundleType.AssetBundle;
    }

    public string GetJsonType(FileStream file)
    {
        if (file.Length < 2)
        {
            return null;
        }
        using var reader = new BinaryReader(file, Encoding.UTF8, leaveOpen: true);

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

        if (content.Contains("\"Months\"") || content.Contains("\"Bullet1\""))
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

    public bool IsPng(FileStream file)
    {
        using var reader = new BinaryReader(file, Encoding.UTF8, leaveOpen: true);

        var magic = reader.ReadBytes(4);

        file.Position = 0;

        return magic[0] == 0x89
            && magic[1] == 0x50
            && magic[2] == 0x4E
            && magic[3] == 0x47;
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
        "UIWidget",
        "cQuest",
        "Game",
        "Fish",
        "Announcement",
        "LevelUp",
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
    private static Regex treeNodeRegex = new(@"^\w+(?:\s=\s.+)?$", RegexOptions.Singleline);

    public string GetTreeNodeType(FileStream file)
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

        var headerString = Encoding.UTF8.GetString(header, offset, 32 - offset).Trim();
        var lineEnding = headerString.Contains("\r\n") ? "\r\n" : "\n";
        var fileType = headerString.Split(lineEnding)[0].Trim();

        if (!treeNodeRegex.IsMatch(fileType))
        {
            return null;
        }

        using var streamReader = new StreamReader(file, Encoding.UTF8, leaveOpen: true);
        var content = streamReader.ReadToEnd();
        file.Position = 0;

        var treeNode = TreeNode.Read(content);

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
        else if (treeNode.Children.Count > 0
            && treeNode.Name == "Root"
            && treeNode.Children.Exists(
                child => child is TreeNode.StringKeyedObject stringNode
                && stringNode.Name == "Script"
            )
        )
        {
            return "RewardSequence";
        }

        return null;
    }

    private bool IsAudioFile(FileStream file)
    {
        using var reader = new BinaryReader(file, Encoding.UTF8, leaveOpen: true);

        var magic = reader.ReadBytes(4);

        file.Position = 0;

        return magic[0] == 0x4F
            && magic[1] == 0x67
            && magic[2] == 0x67
            && magic[3] == 0x53;
    }
}
