/// <summary>
/// 公共引用计数类
/// </summary>
namespace LinkGo.Common.Utils
{
    public class SimpleRefCounter : IRefCounter
    {
        public SimpleRefCounter()
        {
            RefCount = 0;
        }

        public int RefCount { get; private set; }

        public void Retain()
        {
            ++RefCount;
        }

        public void Release()
        {
            --RefCount;
            if (RefCount == 0)
            {
                OnZeroRef();
            }
        }

        protected virtual void OnZeroRef()
        {
        }
    }
}
