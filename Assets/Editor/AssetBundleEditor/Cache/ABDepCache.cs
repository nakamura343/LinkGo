using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

namespace ABBuildEditor
{
    public class DepCacheData
    {
        //资源路径
        public string assetPath;            
        //资源hash(source asset path, source asset, meta file, target platform and importer version)
        public Hash128 hash128;
        //资源直接依赖
        public string[] depsPath;           
    }

    public static class ABDepCache
    {
        private static bool s_Inited = false;

        private static bool s_Dirty = false;

        private static string CACHE_PATH = Application.temporaryCachePath + @"/abdepcache.ac";

        private static Dictionary<string, DepCacheData> s_ABDepCacheMap;

        public static void Open()
        {
            if(!s_Inited)
            {
                s_ABDepCacheMap = ReadFromCache();
                s_Inited = true;
            }
        }

        public static void Close()
        {
            if (!s_Inited)
            {
                Debug.LogWarning("CacheManager don't init. please call CacheManager.Open()");
                return;
            }
            if (s_Dirty)
            {
                WriteToChache(s_ABDepCacheMap);
            }
            s_ABDepCacheMap.Clear();
            s_ABDepCacheMap = null;
            s_Inited = false;
        }

        public static bool TryGetDeps(string assetPath, out string[] deps)
        {
            deps = null;
            if (!s_Inited)
            {
                Debug.LogWarning("CacheManager don't init. please call CacheManager.Open()");
                return false;
            }

            //计算资源的hash128
            Hash128 hash128 = AssetDatabase.GetAssetDependencyHash(assetPath);
            
            DepCacheData data = null;
            if (s_ABDepCacheMap.TryGetValue(assetPath, out data) 
                && data.hash128 == hash128)
            {
                deps = data.depsPath;
                return true;
            }
            return false;
        }

        public static void AddDeps(string assetPath, string[] deps)
        {
            if (!s_Inited)
            {
                Debug.LogWarning("CacheManager don't init. please call CacheManager.Open()");
                return;
            }

            Hash128 hash128 = AssetDatabase.GetAssetDependencyHash(assetPath);
            DepCacheData data;
            if (!s_ABDepCacheMap.TryGetValue(assetPath, out data))
            {
                data = new DepCacheData();
                data.assetPath = assetPath;
                data.hash128 = hash128;
                data.depsPath = deps;
                s_ABDepCacheMap.Add(assetPath, data);
                s_Dirty = true;
            }
            else
            {
                //更新hash128以及依赖deps;
                data.hash128 = hash128;
                data.depsPath = deps;
            }
        }

        public static void RemoveDeps(string assetPath)
        {
            if (!s_Inited)
            {
                Debug.LogWarning("CacheManager don't init. please call CacheManager.Open()");
                return;
            }
            s_ABDepCacheMap.Remove(assetPath);
            s_Dirty = true;
        }

        #region Interval
        //读取缓存
        static Dictionary<string, DepCacheData> ReadFromCache()
        {
            Dictionary<string, DepCacheData> cacheDict = new Dictionary<string, DepCacheData>();
            if (File.Exists(CACHE_PATH))
            {
                //反序列化数据
                using (FileStream fs = File.OpenRead(CACHE_PATH))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    cacheDict = (Dictionary<string, DepCacheData>)bf.Deserialize(fs);
                }
            }
            return cacheDict;
        }

        //写入缓存
        static void WriteToChache(Dictionary<string, DepCacheData> cacheDict)
        {
            if (File.Exists(CACHE_PATH))
                File.Delete(CACHE_PATH);

            //序列化
            using (FileStream fs = File.OpenWrite(CACHE_PATH))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, cacheDict);
            }
        }
        #endregion
    }
}


