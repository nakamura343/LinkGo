using System;
using System.Collections.Generic;

namespace Quark.Common.AssetBundle
{
    public class BundleManifest
    {
        public Dictionary<string, BundleInfo> m_BundleMap;

        public BundleInfo GetBundleByName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException();
            }

            BundleInfo bundle;
            if(m_BundleMap != null && m_BundleMap.TryGetValue(name, out bundle))
            {
                return bundle;
            }
            return null;
        }

        public string[] GetDependencies(string name)
        {
            BundleInfo bundle = GetBundleByName(name);
            if(bundle == null)
            {
                return null;
            }
            return bundle.dependencies;
        }
    }

    public class BundleInfo
    {
        private string m_name;
        public string name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

        private string[] m_dependencies;

        public string[] dependencies
        {
            get
            {
                return m_dependencies;
            }

            set
            {
                m_dependencies = value;
            }
        }
    }
}
