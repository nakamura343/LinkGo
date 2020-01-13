using UnityEngine;

namespace LinkGo.Common.Loader
{
    public class ResourceLoader : BaseLoader
    {
        ResourceRequest m_AsyncOperation;

        public ResourceLoader(string path, int priority) : base(path, priority)
        {

        }

        public override void Start()
        {
            m_AsyncOperation = Resources.LoadAsync(Path);
        }

        public override bool Update()
        {
            if (m_AsyncOperation.isDone)
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
            AssetObj = m_AsyncOperation.asset;
            onCompleted?.Invoke(ELoaderType.ResourceLoader, AssetObj);
        }
    }
}

