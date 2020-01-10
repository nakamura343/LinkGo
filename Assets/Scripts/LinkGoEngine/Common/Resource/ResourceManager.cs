using System;
using UnityEngine;
using LinkGo.Common.Utils;

namespace LinkGo.Common.Resource
{
    public enum EAssetType
    {
        None = 0,
        Shader,         // .shader or build-in shader with name
        Font,           // .ttf
        Texture,        // .tga, .png, .jpg, .tif, .psd, .exr
        Material,       // .mat
        Animation,      // .anim
        Controller,     // .controller
        TextAsset,      // .txt, .bytes
        Prefab,         // .prefab
        Scene,          // .unity
    }

    public class ResourceManager : MonoSingleton<ResourceManager>
    {
        public static bool SimulateAssetBundleInEditor = true;

        #region Interface
        /// <summary>
        /// 初始化资源加加载器
        /// </summary>
        /// <param name="callback"></param>
        public void Initialize(Action<bool> callback)
        {

        }

        public T LoalAsync<T>(string path) where T : BaseAsset
        {
            return default(T);
        }

        public void Unload<T>(T asset) where T : BaseAsset
        {

        }
        #endregion
    }
}


