using System;

namespace LinkGo.Common.Logger
{
    /// <summary>
    /// 输出日志到远程;
    /// </summary>
    public class RemoteTarget : BaseTarget
    {
        public string Url { private set; get; }

        public RemoteTarget(string url)
        {
            Url = url;
        }

        public override void OutputLog(LogType type, string log)
        {
            throw new NotImplementedException();
        }
    }
}
