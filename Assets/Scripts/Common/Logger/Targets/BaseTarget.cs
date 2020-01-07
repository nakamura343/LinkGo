
namespace LinkGo.Common.Logger
{
    /// <summary>
    /// 日志输出目标基类
    /// </summary>
    public abstract class BaseTarget
    {
        public abstract void OutputLog(LogType type, string message);
    }
}


