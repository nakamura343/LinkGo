using System;
using UnityEngine;

namespace LinkGo.Common.Loader
{
    public delegate void LoadAssetCompleted(ELoaderType type, UnityEngine.Object obj);

    /// <summary>
    /// 加载基类
    /// </summary>
    public abstract class BaseLoader : IComparable<BaseLoader>
    {
        public string Path { get; protected set; }

        public int Priority { get; protected set; }

        public UnityEngine.Object AssetObj { get; protected set; }

        public float Progress { get; protected set; }

        public bool IsDone { get; protected set; }

        public LoadAssetCompleted onCompleted;

        public BaseLoader(string path, int priority)
        {
            Path = path;
            Priority = priority;
        }

        public abstract void Start();

        public abstract bool Update();

        public abstract void End();

        public int Compare(BaseLoader x, BaseLoader y)
        {
            return y.Priority - x.Priority;
        }

        public int CompareTo(BaseLoader other)
        {
            if (null == other)
            {
                return 1;//空值比较大，返回1
            }
            return this.Priority.CompareTo(other.Priority);//升序
            //return other.Priority.CompareTo(this.Priority);//降序
        }
    }
}


