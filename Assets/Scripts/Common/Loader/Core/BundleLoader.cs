using UnityEngine;

namespace LinkGo.Common.Loader
{
    public class BundleLoader : BaseLoader
    {
        private AssetBundleCreateRequest m_AsyncOperation;

        public BundleLoader(string path) : base(path)
        {
        }

        public override void Start()
        {
            m_AsyncOperation = AssetBundle.LoadFromFileAsync(Path);
        }

        public override bool Update()
        {
            if(m_AsyncOperation.isDone)
            {
                Progress = 1f;
                IsDone = true;
                End();
                return true;
            }
            Progress = m_AsyncOperation.progress;
            IsDone = false;
            return false;
        }

        public override void End()
        {
            //baseAsset = new BundleAsset(m_AsyncOperation.assetBundle);
        }
    }
}


