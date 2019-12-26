using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinkGo.Common.Resource
{
    public enum ELoadState
    {
        Unknown = 0,
        NotLoaded = 1,
        Loading = 2,
        Loaded = 3,
    }

    public abstract class BaseLoader
    {
        public string Path { get; protected set; }

        public BaseAsset baseAsset { get; protected set; }

        public ELoadState State { get; protected set; }

        public float Progress { get; protected set; }

        public bool IsDone { get; protected set; }

        public BaseLoader(string path)
        {
            Path = path;
        }

        public abstract void Start();

        public abstract bool Update();

        public abstract void End();
    }
}


