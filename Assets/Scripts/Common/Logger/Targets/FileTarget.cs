
namespace LinkGo.Common.Logger
{
    /// <summary>
    /// 输出日志到文件
    /// </summary>
    public class FileTarget : BaseTarget
    {
        public string Path { get; private set; }

        public FileTarget(string path)
        {
            Path = path;
        }

        public override void OutputLog(LogType type, string log)
        {
            //throw new System.NotImplementedException();
        }
    }
}


