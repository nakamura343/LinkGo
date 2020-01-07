
namespace LinkGo.Common.Logger
{
    public enum TargetType
    {
        ConsoleTarget = 1,
        FileTarget = 2,
        RemoteTarget = 3,
    }

    /// <summary>
    /// 日志输出目标基类
    /// </summary>
    public abstract class BaseTarget
    {
        public abstract void OutputLog(LogType type, string message);
    }
}


