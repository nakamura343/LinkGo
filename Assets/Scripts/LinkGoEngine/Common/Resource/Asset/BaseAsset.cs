using System;

namespace LinkGo.Common.Resource
{
    public enum ELoadState
    {
        Unknown = 0,
        Init    = 1, //未加载
        Loading = 2, //加载中
        Loaded  = 3, //已加载
        Unload  = 4, //已卸载
    }

    public abstract class BaseAsset
    {
        public int RefCount { protected set; get; }

        public ELoadState State { protected set; get; }

        public bool IsDone { protected set; get; }

        public float Progress { protected set; get; }

        public event Action<BaseAsset> completed;

        public abstract void Load();

        public abstract void Unload();
    }
}


