using LinkGo.Common.Utils;
using System;
using System.Collections.Generic;

namespace LinkGo.Common.Resource
{
    public delegate void OnLoadAssetComplete(BaseAsset asset);

    public class LoaderManager : MonoSingleton<LoaderManager>
    {
        private List<string> m_WaitingLoadAssetList = new List<string>();
        private Dictionary<string, BaseLoader> m_LoadingAssetMap = new Dictionary<string, BaseLoader>();

        public void LoadAsset(string path, OnLoadAssetComplete onLoadAsset)
        {
            if(string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path is null or empty!");
            }

            if(m_WaitingLoadAssetList.Contains(path))
            {
                return;
            }




        }

        private void Update()
        {
            
        }

    }
}


