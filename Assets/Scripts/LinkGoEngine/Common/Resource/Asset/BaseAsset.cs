using LinkGo.Common.Utils;
using System;
using System.Collections;

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

    public abstract class BaseAsset : SimpleRefCounter, IEnumerator
    {
        public string Path { protected set; get; }

        public int RefCount { protected set; get; }

        public ELoadState State { protected set; get; }

        public bool IsDone { protected set; get; }

        public float Progress { protected set; get; }

        public Action<BaseAsset> completed;

        public BaseAsset(string path)
        {
            Path = path;
            State = ELoadState.Init;
        }

        #region IEnumerator
        public object Current
        {
            get { return null; }
        }

        public bool MoveNext()
        {
            return !IsDone;
        }

        public void Reset()
        {
            
        }
        #endregion

        public abstract bool Update();

        public abstract T GetAsset<T>() where T : UnityEngine.Object;
    }
}


