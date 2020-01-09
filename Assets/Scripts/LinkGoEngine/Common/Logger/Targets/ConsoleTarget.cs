//日志模块
namespace LinkGo.Common.Logger
{
    /// <summary>
    /// 输出日志到Unity控制台
    /// </summary>
    public class ConsoleTarget : BaseTarget
    {
        public override void OutputLog(LogType type, string log)
        {
            string msg = ColorFormatter.LogFormat(type, log);
            UnityEngine.Debug.Log(msg);
        }
    }
}


