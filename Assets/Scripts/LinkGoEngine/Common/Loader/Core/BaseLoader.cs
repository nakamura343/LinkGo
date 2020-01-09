using System;

namespace LinkGo.Common.Loader
{
    public delegate void LoadAssetCompleted(BaseObject obj);

    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseLoader
    {
        public string Path { get; protected set; }

        public BaseObject baseObject { get; protected set; }

        public float Progress { get; protected set; }

        public bool IsDone { get; protected set; }

        public LoadAssetCompleted onCompleted;

        public BaseLoader(string path)
        {
            Path = path;
        }

        public abstract void Start();

        public abstract bool Update();

        public abstract void End();
    }
}


