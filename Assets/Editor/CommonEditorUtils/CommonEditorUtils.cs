using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// Unity编辑器的Common工具类
/// </summary>
public class CommonEditorUtils
{
    public static T FindAsset<T>() where T : UnityEngine.Object
    {
        string filter = string.Format("t:{0}", typeof(T));
        return FindAsset<T>(filter, null);
    }

    public static T FindAsset<T>(string filter, string[] searchInFolders) where T : UnityEngine.Object
    {
        T t = default(T);
        string[] assetGUIDs = AssetDatabase.FindAssets("t:ScriptTemplateSetting", searchInFolders);
        if (assetGUIDs != null && assetGUIDs.Length > 0)
        {
            int index = 0;
            do
            {
                string path = AssetDatabase.GUIDToAssetPath(assetGUIDs[index]);
                t = AssetDatabase.LoadAssetAtPath<T>(path);
                index++;
            }
            while (t == null && index < assetGUIDs.Length);
        }
        return t;
    }

    public static List<T> FindAssets<T>(string filter, string[] searchInFolders) where T : UnityEngine.Object
    {
        List<T> ts = new List<T>();
        T t = default(T);
        string[] guids = AssetDatabase.FindAssets(filter, searchInFolders);
        if (guids != null && guids.Length > 0)
        {
            foreach(var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                t = AssetDatabase.LoadAssetAtPath<T>(path);
                if(t != null)
                {
                    ts.Add(t);
                }
            }
        }
        return ts;
    }
}
