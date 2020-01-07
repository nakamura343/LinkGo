//��־ģ��
namespace LinkGo.Common.Logger
{
    /// <summary>
    /// �����־��Unity����̨
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


