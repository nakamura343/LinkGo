using LinkGo.Common.Loader;
using System;

namespace LinkGo.Common.Resource
{
    public class SimpleAsset : BaseAsset
    {
        private string m_assetName;

        public SimpleAsset(string path, string assetName) : base(path)
        {
            m_assetName = assetName;
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


