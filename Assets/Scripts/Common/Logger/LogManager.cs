using System;
using System.Text;
using System.Collections.Generic;

namespace LinkGo.Common.Logger
{
    public class LogManager
    {
        private static StringBuilder s_LogBuilder = new StringBuilder();

        private static Dictionary<string, Logger> s_LogMap = new Dictionary<string, Logger>();

        public static Logger GetLogger(string tag)
        {
            Logger log;
            if(!s_LogMap.TryGetValue(tag, out log))
            {
                //添加一个基本的logger
                log = new Logger(tag, LogType.All, true);
                log.AddTarget(new ConsoleTarget());
                s_LogMap.Add(tag, log);
            }
            return log;
        }

        public static string LogFormat(string tag, LogType type, string format, params object[] args)
        {
            s_LogBuilder.Clear();
            s_LogBuilder.AppendFormat("Tag:{0} Type:{1} >{2}", tag, type, string.Format(format, args));
            return s_LogBuilder.ToString();
        }

        public static string LogFormat2Time(string tag, LogType type, string format, params object[] args)
        {
            s_LogBuilder.Clear();
            s_LogBuilder.AppendFormat("[{0}] Tag:{1} Type:{2} >{3}", DateTime.Now, tag, type, string.Format(format, args));
            return s_LogBuilder.ToString();
        }
    }
}


