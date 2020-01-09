using UnityEngine;

namespace LinkGo.Common.Loader
{
    public class BundleObject : BaseObject
    {
        public AssetBundle assetBundle;

        public BundleObject(AssetBundle bundle)
        {
            assetBundle = bundle;
        }
    }
}

