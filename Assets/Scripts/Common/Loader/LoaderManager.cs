using LinkGo.Common.Utils;
using System;
using System.Collections.Generic;

namespace LinkGo.Common.Loader
{
    

    public class LoaderManager : MonoSingleton<LoaderManager>
    {
        private List<string> m_WaitingLoadAssetList = new List<string>();
        private Dictionary<string, BaseLoader> m_LoadingAssetMap = new Dictionary<string, BaseLoader>();

        public BaseLoader LoadAsset(string path)
        {
            if(string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path is null or empty!");
            }

            if(m_WaitingLoadAssetList.Contains(path))
            {
                return null;
            }
            return null;
        }

        private void Update()
        {
            
        }

    }
}


