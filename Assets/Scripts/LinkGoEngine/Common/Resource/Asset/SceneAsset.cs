using LinkGo.Common.Loader;
using System;

namespace LinkGo.Common.Resource
{
    public class SceneAsset : BaseAsset
    {
        private string m_sceneName;
        private bool m_isAdditive;

        public SceneAsset(string path, string sceneName, bool isAdditive) : base(path)
        {
            m_sceneName = sceneName;
            m_isAdditive = isAdditive;
        }

        public override T GetAsset<T>()
        {
            return default(T);
        }

        public override void Load(ELoaderType type)
        {
            
        }

        public override void Unload()
        {
            
        }

        public override bool Update()
        {
            return false;
        }
    }
}
