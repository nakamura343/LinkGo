using UnityEngine;

namespace LinkGo.Common.Loader
{
    public class ResourceObject : BaseObject
    {
        public Object obj;

        public ResourceObject(Object obj)
        {
            this.obj = obj;
        }
    }
}

