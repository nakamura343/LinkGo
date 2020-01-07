using System;

namespace LinkGo.Common.Logger
{
    /// <summary>
    /// 输出日志到远程;
    /// </summary>
    public class RemoteTarget : BaseTarget
    {
        public override void OutputLog(LogType type, string log)
        {
            throw new NotImplementedException();
        }
    }
}
