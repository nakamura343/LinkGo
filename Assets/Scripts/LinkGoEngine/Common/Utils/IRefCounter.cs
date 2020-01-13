/// <summary>
/// 公共引用计数接口类
/// </summary>
namespace LinkGo.Common.Utils
{
    public interface IRefCounter
    {
        int RefCount { get; }

        void Retain();

        void Release();
    }
}
