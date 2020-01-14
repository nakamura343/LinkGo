using System.IO;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteAtlasGenerator
{
    [MenuItem("Tools/UI/SpriteAtlasGenAll")]
    static void SpriteAtlasGenAll()
    {
        string spriteAtlasDir = EditorUtility.OpenFolderPanel("打包SpriteAtlas", Application.dataPath, "");
        if(string.IsNullOrEmpty(spriteAtlasDir))
        {
            EditorUtility.DisplayDialog("错误", "选择正确的需要生成SpriteAtlas的Root目录", "ok");
            return;
        }

        string[] subDirs = Directory.GetDirectories(spriteAtlasDir);
        foreach(var dir in subDirs)
        {
            string relativeDir = FileUtil.GetProjectRelativePath(dir);
            AutoSetAtlasContents(relativeDir);
        }
        AssetDatabase.Refresh();
    }

    [MenuItem("Assets/Tools/UI/SpriteAtlasGen", false, 0)]
    static void SpriteAtlasGen()
    {
        Object obj = Selection.activeObject;
        string dir = AssetDatabase.GetAssetPath(obj);
        if(AssetDatabase.IsValidFolder(dir))
        {
            AutoSetAtlasContents(dir);
            AssetDatabase.Refresh();
        }
        else
        {
            EditorUtility.DisplayDialog("错误", "选择正确的需要生成SpriteAtlas的目录","ok");
        }
    }

    static void AutoSetAtlasContents(string dir)
    {
        SpriteAtlas atlas = new SpriteAtlas();

        // 设置参数 可根据项目具体情况进行设置
        SpriteAtlasPackingSettings packSetting = new SpriteAtlasPackingSettings()
        {
            blockOffset = 1,
            enableRotation = false,
            enableTightPacking = false,
            padding = 2,
        };
        atlas.SetPackingSettings(packSetting);

        SpriteAtlasTextureSettings textureSetting = new SpriteAtlasTextureSettings()
        {
            readable = false,
            generateMipMaps = false,
            sRGB = true,
            filterMode = FilterMode.Bilinear,
        };
        atlas.SetTextureSettings(textureSetting);

        TextureImporterPlatformSettings platformSetting = new TextureImporterPlatformSettings()
        {
            maxTextureSize = 1024,
            format = TextureImporterFormat.Automatic,
            crunchedCompression = true,
            textureCompression = TextureImporterCompression.Compressed,
            compressionQuality = 50,
        };
        atlas.SetPlatformSettings(platformSetting);

        DirectoryInfo dirInfo = new DirectoryInfo(dir);
        string path = $"{dir}/{dirInfo.Name}.spriteatlas";
        if(File.Exists(path))
        {
            File.Delete(path);
        }

        AssetDatabase.CreateAsset(atlas, path);

        //1.添加文件
        FileInfo[] files = dirInfo.GetFiles("*.png");
        foreach (FileInfo file in files)
        {
            atlas.Add(new[] { AssetDatabase.LoadAssetAtPath<Sprite>($"{dir}/{file.Name}") });
        }

        //2.添加子文件夹
        DirectoryInfo[] subdirs = dirInfo.GetDirectories();
        foreach (DirectoryInfo subdir in subdirs)
        {
            Object obj = AssetDatabase.LoadAssetAtPath(subdir.FullName, typeof(Object));
            atlas.Add(new[] { obj });
        }

        AssetDatabase.SaveAssets();
        Debug.LogFormat("Generate a <b>{0}.spriteatlas</b> success! path=<b>{1}</b>", 
            dirInfo.Name, path);
    }
}
