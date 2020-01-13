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
            throw new NotImplementedException();
        }

        public override bool Update()
        {
            throw new NotImplementedException();
        }
    }
}
