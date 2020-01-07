using System.Collections.Generic;

namespace LinkGo.Common.Logger
{
    /// <summary>
    /// 定义日志级别
    /// </summary>
    public enum LogType
    {
        None  = 0,  //屏蔽全部日志
        Trace = 1,  //特别细节的日志，可能包含大量的信息(开发期启用)
        Debug = 2,  //Debug信息，比Trace的信息量要少一些，通常不会在生产环境下使用。
        Info  = 4,  //信息消息，这个是在生产环境下经常使用到的。
        Warn  = 8,  //警告消息，通常用来指示非关键性的问题，这些问题可以被恢复或者只是临时错误。
        Error = 16, //错误消息 - 大多数时候，他们都是Exception（异常）
        Fatal = 32, //特别严重的错误！
        All   = 127 //打印全部日志
    }

    /// <summary>
    /// 日志类
    /// </summary>
    public class Logger
    {
        public string Tag { private set; get; }

        public LogType Type { private set; get; }

        public bool ShowTime { private set; get; }

        public List<BaseTarget> Targets { private set; get; }

        public Logger(string tag, LogType type, bool showTime)
        {
            Tag = tag;
            Type = type;
            ShowTime = showTime;
            Targets = new List<BaseTarget>();
        }

        public Logger AddTarget(BaseTarget target)
        {
            if (target != null)
            {
                Targets.Add(target);
            }
            return this;
        }

        public void Trace(string format, params object[] args)
        {
            if((Type & LogType.Trace) != 0)
            {
                Log(Tag, LogType.Trace, format, args);
            }
        }

        public void Debug(string format, params object[] args)
        {
            if ((Type & LogType.Debug) != 0)
            {
                Log(Tag, LogType.Debug, format, args);
            }
        }

        public void Info(string format, params object[] args)
        {
            if ((Type & LogType.Info) != 0)
            {
                Log(Tag, LogType.Info, format, args);
            }
        }

        public void Warn(string format, params object[] args)
        {
            if ((Type & LogType.Warn) != 0)
            {
                Log(Tag, LogType.Warn, format, args);
            }
        }

        public void Error(string format, params object[] args)
        {
            if ((Type & LogType.Error) != 0)
            {
                Log(Tag, LogType.Error, format, args);
            }
        }

        public void Fatal(string format, params object[] args)
        {
            if ((Type & LogType.Fatal) != 0)
            {
                Log(Tag, LogType.Fatal, format, args);
            }
        }

        private void Log(string tag, LogType type, string format, params object[] args)
        {
            string message = GetMessage(tag, type, format, args);
            foreach (var target in Targets)
            {
                target.OutputLog(type, message);
            }
        }

        private string GetMessage(string tag, LogType type, string format, params object[] args)
        {
            if(!ShowTime)
            {
                return LogManager.LogFormat(tag, type, format, args);
            }
            else
            {
                return LogManager.LogFormat2Time(tag, type, format, args);
            }
        }
    }
}


