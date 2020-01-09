
namespace LinkGo.Common.Logger
{
    public enum TargetType
    {
        ConsoleTarget = 1,
        FileTarget = 2,
        RemoteTarget = 3,
    }

    /// <summary>
    /// ��־���Ŀ�����
    /// </summary>
    public abstract class BaseTarget
    {
        public abstract void OutputLog(LogType type, string message);
    }
}


