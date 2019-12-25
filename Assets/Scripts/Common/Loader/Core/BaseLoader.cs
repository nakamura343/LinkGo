using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinkGo.Common.Loader
{
    public enum ELoadState
    {
        Unknown = 0,
        NotLoaded = 1,
        Loading = 2,
        Loaded = 3,
    }

    public abstract class BaseLoader : IEnumerator
    {
        public object Current => throw new System.NotImplementedException();

        public bool MoveNext()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }
    }
}


