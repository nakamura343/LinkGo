using LinkGo.Common.Loader;
using LinkGo.Common.Utils;
using System;
using System.Collections.Generic;

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
        public static AssetManifest m_DepManifests = null;


        private Dictionary<string, BaseAsset> m_AssetDict;

        #region Interface
        /// <summary>
        /// 初始化资源加加载器
        /// </summary>
        /// <param name="callback"></param>
        public void Initialize(Action callback)
        {
            if(!SimulateAssetBundleInEditor)
            {
                //m_DepManifests = DepManager.Init();
            }
            m_AssetDict = new Dictionary<string, BaseAsset>();
            callback?.Invoke();
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="path">资源的平台路径</param>
        /// <param name="assetName">资源名称</param>
        /// <returns>SimpleAsset</returns>
        public SimpleAsset LoalAssetAsync(string path, string assetName)
        {
            BaseAsset asset;
            if (!m_AssetDict.TryGetValue(path, out asset))
            {
                asset = new SimpleAsset(path, assetName);
                //asset.Load(SimulateAssetBundleInEditor);
            }
            else
            {
                asset.Retain();
            }
            return asset as SimpleAsset;
        }

        /// <summary>
        /// 加载场景资源
        /// </summary>
        /// <param name="path">资源的平台路径</param>
        /// <param name="sceneName">场景名称</param>
        /// <param name="isAdditive">是否以附加的形式加载场景</param>
        /// <returns>SceneAsset</returns>
        public SceneAsset LoalSceneAsync(string path, string sceneName, bool isAdditive)
        {
            BaseAsset asset;
            if(!m_AssetDict.TryGetValue(path, out asset))
            {
                asset = new SceneAsset(path, sceneName, isAdditive);
                //asset.Load(SimulateAssetBundleInEditor);
            }
            else
            {
                //asset.Retain();
            }
            return asset as SceneAsset;
        }

        /// <summary>
        /// 卸载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="asset"></param>
        public void Unload<T>(T asset) where T : BaseAsset
        {
            if (m_AssetDict.ContainsKey(asset.Path))
            {
                //asset.Unload();
                if(asset.RefCount <= 0)
                {
                    m_AssetDict.Remove(asset.Path);
                }
            }
        }
        #endregion

        #region Intrerval
        private void Update()
        {
            
        }



        #endregion
    }
}


