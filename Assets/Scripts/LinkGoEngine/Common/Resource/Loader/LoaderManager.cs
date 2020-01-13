using System;
using System.Collections.Generic;

namespace LinkGo.Common.Loader
{
    public enum ELoaderType
    {
        EditorLoader = 1,
        ResourceLoader = 2,
        BundleLoader = 3,
    }
    
    public class LoaderManager
    {
        public static int s_MaxSyncLoader = 8;

        //等待加载的列表;
        private static List<BaseLoader> s_WaitingLoaderList;

        //正在加载的列表;
        private static List<BaseLoader> s_LoadingAssetDict;

        public static void Initialize(int maxSyncLoader)
        {
            s_MaxSyncLoader = maxSyncLoader;

            s_WaitingLoaderList = new List<BaseLoader>(16);
            s_LoadingAssetDict = new List<BaseLoader>(8);
        }

        public static BaseLoader Load(ELoaderType type, string path, int priority = 0)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path is null or empty!");
            }

            BaseLoader loader;
            //1.在正在加载的列表中;
            if (FindBaseLoader(path, s_LoadingAssetDict, out loader))
            {
                return loader;
            }

            //2.在等待加载的列表中;
            if (FindBaseLoader(path, s_WaitingLoaderList, out loader))
            {
                return loader;
            }

            //3.新加入加载的;
            loader = CreateLoader(type, path, priority);
            s_WaitingLoaderList.Add(loader);
            if(s_WaitingLoaderList.Count > 0)
            {
                s_WaitingLoaderList.Sort();
            }
            return loader;
        }

        static bool FindBaseLoader(string path, List<BaseLoader> list, out BaseLoader loader)
        {
            loader = null;
            foreach(var load in list)
            { 
                if(load.Path.Equals(path))
                {
                    loader = load;
                    return true;
                }
            }
            return false;
        }

        static BaseLoader CreateLoader(ELoaderType type, string path, int priority)
        {
            BaseLoader loader;
           
            switch (type)
            {
                case ELoaderType.EditorLoader:
                    loader = new EditorLoader(path, priority);
                    break;
                case ELoaderType.ResourceLoader:
                    loader = new ResourceLoader(path, priority);
                    break;
                case ELoaderType.BundleLoader:
                    loader = new BundleLoader(path, priority);
                    break;
                default:
                    throw new ArgumentNullException("ELoaderType is not exist! Type:" + type);
            }
            return loader;
        }

        public static void Update()
        {
            for(int i = 0; i < s_LoadingAssetDict.Count;)
            {
                if(s_LoadingAssetDict[i].Update())
                {
                    s_LoadingAssetDict.RemoveAt(i);
                    continue;
                }
                i++;
            }

            int loadlen = s_LoadingAssetDict.Count;
            int waitlen = s_WaitingLoaderList.Count;
            while (loadlen < s_MaxSyncLoader && waitlen > 0)
            {
                BaseLoader loader = s_WaitingLoaderList[waitlen - 1];
                loader.Start();

                s_LoadingAssetDict.Add(loader);
                loadlen++;

                s_WaitingLoaderList.RemoveAt(waitlen - 1);
                waitlen--;
            }
        }
    }
}


